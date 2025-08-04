using System.ComponentModel.DataAnnotations;

namespace GeradorDeTestes.Dominio.ModuloMateria;

public enum SerieDaMateria
{
    [Display(Name = "Primeiro Ano")] PrimeiroAno = 1,
    [Display(Name = "Segundo Ano")] SegundoAno = 2,
    [Display(Name = "Terceiro Ano")] TerceiroAno = 3,
    [Display(Name = "Quarto Ano")] QuartoAno = 4,
    [Display(Name = "Quinto Ano")] QuintoAno = 5,
    [Display(Name = "Sexto Ano")] SextoAno = 6,
    [Display(Name = "Setimo Ano")] SetimoAno = 7,
    [Display(Name = "Oitavo Ano")] OitavoAno = 8
}