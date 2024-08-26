using System;

namespace FileMerger {
    class Program {
        static void Main(string[] args) {
            Console.Clear(); // Limpa a tela para iniciar com uma interface limpa
            PrintHeader(); // Exibe o cabeçalho personalizado

            // Cria instâncias das classes que gerenciam as operações
            UserInputHandler userInputHandler = new UserInputHandler();
            PdfMerger pdfMerger = new PdfMerger();
            MobiConverter mobiConverter = new MobiConverter();

            // Solicita ao usuário o caminho da pasta dos PDFs
            string folderPath = userInputHandler.GetFolderPathWithRetry();

            if (!string.IsNullOrEmpty(folderPath)) {
                // Realiza a mesclagem dos PDFs
                string outputPdfPath = pdfMerger.MergePdfs(folderPath);

                if (!string.IsNullOrEmpty(outputPdfPath)) {
                    // Pergunta ao usuário se deseja converter para .mobi
                    if (userInputHandler.AskForMobiConversion()) {
                        // Solicita ao usuário o caminho da pasta do Calibre
                        string calibrePath = userInputHandler.GetCalibrePathWithRetry();

                        if (!string.IsNullOrEmpty(calibrePath)) {
                            mobiConverter.ConvertPdfToMobi(outputPdfPath, calibrePath);
                        }
                    }
                }
            }

            PrintFooter(); // Exibe o rodapé personalizado
            Console.ReadKey();
        }

        // Método para exibir o cabeçalho do programa
        static void PrintHeader() {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═════════════════════════════════════════════╗");
            Console.WriteLine("║    Seja bem-vindo(a) ao File Merger v1.0    ║");
            Console.WriteLine("║             by Lucas Oliveira.              ║");
            Console.WriteLine("╚═════════════════════════════════════════════╝");
            Console.WriteLine("   ____       _     _                 _             ");
            Console.WriteLine("  / ___| ___ | | __| | ___ _ __  _ __(_)_ __   ___  ");
            Console.WriteLine(" | |  _ / _ \\| |/ _` |/ _ \\ '_ \\| '__| | '_ \\ / _ \\ ");
            Console.WriteLine(" | |_| | (_) | | (_| |  __/ | | | |  | | | | | (_) |");
            Console.WriteLine("  \\____|\\___/|_|\\__,_|\\___|_| |_|_|  |_|_| |_|\\___/ ");
            Console.ResetColor();
            Console.WriteLine("\n");
        }

        // Método para exibir o rodapé do programa
        static void PrintFooter() {
            Console.WriteLine("\n╔══════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                  ║");
            Console.WriteLine("║       Obrigado por usar o FileMerger!            ║");
            Console.WriteLine("║  Pressione qualquer tecla para sair...           ║");
            Console.WriteLine("║                                                  ║");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");
            Console.ReadKey();
        }
    }
}
