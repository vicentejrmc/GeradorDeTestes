using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Infraestrutura.IAGemini.DTOs;
public class QuestaoDto
{
    public string Enunciado { get; set; }
    public List<AlternativaDto> Alternativas { get; set; }

    public QuestaoDto() { }
}

//DTOs é uma pratica comum e recomenadada:
// classe serve como adaptador para nao passar os dados recebidos diretamente para a Entidade
