using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Threading;

namespace FileMerger {
    // Classe responsável por combinar os arquivos PDF
    class PdfMerger {
        // Método que realiza a combinação dos PDFs
        public string MergePdfs(string folderPath) {
            try {
                // Cria um novo documento PDF
                PdfDocument outputDocument = new PdfDocument();

                // Obtém todos os arquivos PDF na pasta especificada, em ordem crescente
                string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
                Array.Sort(pdfFiles);

                int totalFiles = pdfFiles.Length;
                int processedFiles = 0;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nProcessando arquivos...\n");
                Console.ResetColor();

                foreach (string file in pdfFiles) {
                    // Abre o documento PDF atual
                    PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                    // Adiciona cada página do PDF atual ao documento final
                    foreach (PdfPage page in inputDocument.Pages) {
                        outputDocument.AddPage(page);
                    }

                    processedFiles++;
                    DisplayProgress(processedFiles, totalFiles);
                    Thread.Sleep(500); // Simulação de tempo de processamento para fins de exibição da barra de progresso
                }

                // Define o caminho do arquivo de saída
                string outputPdfPath = Path.Combine(folderPath, "Resultado.pdf");

                // Salva o documento final como um arquivo PDF
                outputDocument.Save(outputPdfPath);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n\n-> Sucesso! Os arquivos PDF foram combinados em: {outputPdfPath}");
                Console.ResetColor();

                return outputPdfPath;
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n** Erro: Ocorreu um erro ao combinar os PDFs: {ex.Message} **");
                Console.ResetColor();
                return null;
            }
        }

        // Método para exibir a barra de progresso estilizada com animação
        private void DisplayProgress(int processedFiles, int totalFiles) {
            int progress = (processedFiles * 100) / totalFiles;
            int barSize = 50; // Tamanho da barra de progresso
            int filledSize = (progress * barSize) / 100;

            string animation = "|/-\\";
            char animChar = animation[processedFiles % animation.Length];

            Console.ForegroundColor = ConsoleColor.Green; // Cor da parte preenchida da barra
            Console.Write($"\r{animChar} [");
            Console.Write(new string('█', filledSize)); // Símbolo de preenchimento
            Console.ForegroundColor = ConsoleColor.DarkGray; // Cor da parte não preenchida da barra
            Console.Write(new string('░', barSize - filledSize)); // Símbolo da parte não preenchida
            Console.ForegroundColor = ConsoleColor.White; // Cor do texto da porcentagem
            Console.Write($"] {progress}%");
        }
    }
}
