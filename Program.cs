
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Pdf;
//using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Security;

using System.Security.Cryptography.X509Certificates;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Licensing;
using System.IO;

//using System.Security;

namespace ConsoleApp_A3
{
    class Program
    {
        static void teste1()
        {
            //Pasta com PDFs
            String pdfFolder = "C:\\Users\\User\\source\\repos\\ConsoleApp-A3\\pdfs";
            //Todos os PDFS
            string[] paths = Directory.GetFiles(pdfFolder);
            //Declarar Licença para utilizar a biblioteca Syncfusion 
            SyncfusionLicenseProvider.RegisterLicense("chave-que-deve-ser-criada-no-site-Syncfusion");

            //Intancinando o repositório físico no qual os certificados são mantidos e gerenciados
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            
            //Printando Certificados
            Console.WriteLine("Number of certificates: {0}{1}", collection.Count, Environment.NewLine);
            //Printando informações sobre os certificados registrados
            foreach(X509Certificate2 item in collection)
            {
               
                Console.WriteLine(item.GetName());
            }
            //Intanciando o certificado na posição 1 que é o meu certificado digital o na posição 0 é referente ao localhost
            X509Certificate2 cert = new X509Certificate2(collection[1]);//Aqui se eu tivesse mais certificados poderia selecionar qualquer um deles.


           

           //Intanciando PDF Certificate para poder assinar os documentos
            PdfCertificate pdfCert = new PdfCertificate(cert);

            //Percorrendo todos os PDFS
            foreach (string path in paths)
            {

                
                //Printando o documento que está sendo assinado!
                Console.WriteLine("Assinando: " + path);
                //Carregando o PDF
                PdfLoadedDocument document = new PdfLoadedDocument(path);
                //Selecionando a pagina que será assinada
                PdfPageBase page = document.Pages[0];
                //Assinar
                PdfSignature signature = new PdfSignature(document, page, pdfCert, "Sign");
              
                //Sobrescreve o documento
                document.Save(path);
                //Libera memória
                document.Close();
            }







        }
       static void listarArquivos()
        {
            String pdfFolder ="C:\\Users\\User\\source\\repos\\ConsoleApp-A3\\pdfs";
            string[] files = Directory.GetFiles(pdfFolder);
      
            Console.WriteLine(files[1]);
       

        }

        static void Main(string[] args)
        {


            teste1();
           // listarArquivos();

            Console.WriteLine("Pressione ENTER para sair...");
            Console.Read();


        }


    }
}


