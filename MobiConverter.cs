using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace FileMerger {
    // Classe responsável por converter o arquivo PDF para .mobi
    class MobiConverter {
        // Método que realiza a conversão do PDF para .mobi usando o Calibre
        public void ConvertPdfToMobi(string pdfFilePath, string calibrePath) {
            try {
                // Define o caminho do arquivo .mobi de saída
                string mobiFilePath = Path.ChangeExtension(pdfFilePath, ".mobi");

                // Comando para chamar o Calibre e converter o PDF para MOBI
                ProcessStartInfo startInfo = new ProcessStartInfo {
                    FileName = Path.Combine(calibrePath, "ebook-convert.exe"),
                    Arguments = $"\"{pdfFilePath}\" \"{mobiFilePath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Console.WriteLine("\nConvertendo para .mobi...\n");

                // Executa o processo de conversão
                using (Process process = Process.Start(startInfo)) {
                    Thread progressThread = new Thread(() => DisplayProgressWhileConverting(process));
                    progressThread.Start();

                    // Captura a saída do processo sem exibi-la, para manter o console limpo
                    process.StandardOutput.ReadToEnd();
                    process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    progressThread.Join(); // Garante que a barra de progresso termine

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n-> Conversão para .mobi concluída com sucesso!");
                    Console.ResetColor();
                }
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"** Erro: Ocorreu um erro ao converter o PDF para MOBI: {ex.Message} **");
                Console.ResetColor();
            }
        }

        // Método para exibir a barra de progresso simulada durante a conversão
        private void DisplayProgressWhileConverting(Process process) {
            int barSize = 50; // Tamanho da barra de progresso
            int progress = 0;

            while (!process.HasExited) {
                progress = (progress + 1) % 101; // Incrementa progressivamente de 0 a 100%
                int filledSize = (progress * barSize) / 100;

                string animation = "|/-\\";
                char animChar = animation[progress % animation.Length];

                Console.ForegroundColor = ConsoleColor.Green; // Cor da parte preenchida da barra
                Console.Write($"\r{animChar} [");
                Console.Write(new string('█', filledSize)); // Símbolo de preenchimento
                Console.ForegroundColor = ConsoleColor.DarkGray; // Cor da parte não preenchida da barra
                Console.Write(new string('░', barSize - filledSize)); // Símbolo da parte não preenchida
                Console.ForegroundColor = ConsoleColor.White; // Cor do texto da porcentagem
                Console.Write($"] {progress}%");

                Thread.Sleep(100); // Simulação de tempo de atualização
            }

            // Finaliza a barra de progresso em 100%
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\r[");
            Console.Write(new string('█', barSize));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"] 100%");
        }
    }
}
