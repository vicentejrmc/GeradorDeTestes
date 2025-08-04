using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Dominio.ModuloTeste;
public interface IGeradorTeste
{
    byte[] GerarNovoTeste(Teste teste, bool gabarito);
}
