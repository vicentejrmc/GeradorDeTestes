using GeradorDeTestes.Dominio.ModuloTeste;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Infraestrutura.Pdf.GeradorDeTestesPdf;
public class GeradorTestePdf : IGeradorTeste
{
    public byte[] GerarNovoTeste(Teste teste, bool gabarito)
    {
        var document = new TesteDocument(teste, gabarito);
        var pdfBytes = document.GeneratePdf();

        return pdfBytes;
    }
}
