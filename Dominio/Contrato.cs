using System;
using System.Globalization;

namespace Dominio
{
    [Serializable]
    public class Contrato
    {
       
        public Contrato(string nomeDoUsuario, int cpf, string emailDoUsuario, string nomeDaEmpresaContratada,
        int cnpjDaEmpresaContratada, int numeroDoContrato, DateTime validadeDoContrato)
        {
            NomeDoUsuario = nomeDoUsuario.ToUpper();
            Cpf = cpf;
            EmailDoUsuario = emailDoUsuario.ToUpper();
            NomeDaEmpresaContratada = nomeDaEmpresaContratada.ToUpper();
            NumeroDoContrato = numeroDoContrato;
            ValidadeDoContrato = validadeDoContrato;
            CnpjDaEmpresaContratada = cnpjDaEmpresaContratada;
            Id = new Guid();
        }

        public Contrato() { }

        CultureInfo cultureInfo = new CultureInfo("pt-BR");
        public Guid Id { get; set; }
        public string NomeDoUsuario { get; set; }
        public int Cpf { get; set; }
        public string EmailDoUsuario { get; set; }
        public string NomeDaEmpresaContratada { get; set; }
        public int NumeroDoContrato { get; set; }
        public DateTime ValidadeDoContrato { get; set; }
        public int CnpjDaEmpresaContratada { get; set; }
        public bool ContratoAtivo
        {

            get
            {
                var diasParaFimDoContrato = (ValidadeDoContrato - DateTime.Now);
                if (diasParaFimDoContrato.TotalDays > 0)
                {
                    return true;
                }

                return false;
            }

        }
        private string EstadoDoContrato()

        {
            if (ContratoAtivo == true)
                return "SIM";
            else return "NÃO";
        }


        public override string ToString()
        {
            return $"Nome Do Usuário: {NomeDoUsuario} \n" +
                $"CPF: {Cpf} \n" +
                $"Email: {EmailDoUsuario} \n" +
                $"Empresa: {NomeDaEmpresaContratada} \n" +
                $"CNPJ: {CnpjDaEmpresaContratada} \n" +
                $"Contato n°: {NumeroDoContrato} \n" +
                $"Contrato válido até: {ValidadeDoContrato:dd/MM/yyyy} \n" +
                $"O Contrato encontra-se Ativo? : {EstadoDoContrato()} \n";
        }

    }
}
