using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace FrontEnd.Models
{
    //cada propriedade recebe suas devidas anotações

    public class User
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Insira um Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Insira um CPF")]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Insira um Email válido")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Insira um Telefone")]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Selecione um Gênero")]
        [Display(Name = "Gênero")]
        public Genero Sexo { get; set; }

        [Required(ErrorMessage = "Selecione a Data de Nascimento")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataNascimento { get; set; }

    }

    //para poder ser manipulado (quando é retornado da api ou enviado para ela, necessita de uma conversão especifica para Enum

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Genero
    {
        Masculino,
        Feminino
    }
}