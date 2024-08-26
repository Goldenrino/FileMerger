using System;
using System.Threading;

namespace FileMerger {
    // Classe responsável por lidar com a entrada do usuário
    class UserInputHandler {
        // Método para solicitar o caminho da pasta ao usuário com possibilidade de tentar novamente
        public string GetFolderPathWithRetry() {
            string folderPath = null;

            while (string.IsNullOrEmpty(folderPath)) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-> Por favor, insira o caminho da pasta com os arquivos PDF:");
                Console.ResetColor();
                Console.Write("   > ");
                folderPath = Console.ReadLine();

                if (!System.IO.Directory.Exists(folderPath)) {
                    DisplayErrorAnimation("** Erro: A pasta especificada não existe. **");
                    if (!AskToRetry()) {
                        return null; // Usuário optou por não tentar novamente
                    }
                    folderPath = null; // Reinicia o loop para tentar novamente
                }
                else {
                    DisplaySuccessAnimation("Pasta encontrada com sucesso!");
                }
            }

            return folderPath;
        }

        // Método para solicitar o caminho da pasta do Calibre ao usuário com possibilidade de tentar novamente
        public string GetCalibrePathWithRetry() {
            string calibrePath = null;

            while (string.IsNullOrEmpty(calibrePath)) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n-> Por favor, insira o caminho da pasta onde o Calibre (ebook-convert.exe) está instalado:");
                Console.ResetColor();
                Console.Write("   > ");
                calibrePath = Console.ReadLine();

                string ebookConvertPath = System.IO.Path.Combine(calibrePath, "ebook-convert.exe");
                if (!System.IO.Directory.Exists(calibrePath) || !System.IO.File.Exists(ebookConvertPath)) {
                    DisplayErrorAnimation("** Erro: O caminho especificado não contém o 'ebook-convert.exe'. **");
                    if (!AskToRetry()) {
                        return null; // Usuário optou por não tentar novamente
                    }
                    calibrePath = null; // Reinicia o loop para tentar novamente
                }
                else {
                    DisplaySuccessAnimation("Pasta do Calibre encontrada com sucesso!");
                }
            }

            return calibrePath;
        }

        // Método para perguntar ao usuário se deseja tentar novamente
        private bool AskToRetry() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n-> Deseja tentar novamente? (s/n):");
            Console.ResetColor();
            Console.Write("   > ");
            string response = Console.ReadLine();

            return response?.ToLower() == "s";
        }

        // Método para perguntar ao usuário se deseja converter o arquivo para .mobi
        public bool AskForMobiConversion() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n-> Deseja converter o arquivo combinado para .mobi? (s/n):");
            Console.ResetColor();
            Console.Write("   > ");
            string response = Console.ReadLine();

            return response?.ToLower() == "s";
        }

        // Animação de erro
        private void DisplayErrorAnimation(string message) {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (char c in message) {
                Console.Write(c);
                Thread.Sleep(50); // Simula a digitação do erro
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        // Animação de sucesso
        private void DisplaySuccessAnimation(string message) {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (char c in message) {
                Console.Write(c);
                Thread.Sleep(50); // Simula a digitação do sucesso
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}