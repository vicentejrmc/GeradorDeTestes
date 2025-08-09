using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Infraestrutura.IAGemini.DTOs;
public class AlternativaDto
{
    public string Resposta { get; set; }
    public bool Correta { get; set; }

    public AlternativaDto() { }
}