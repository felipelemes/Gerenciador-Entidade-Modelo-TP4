using System;

namespace Dominio
{
    public class Entidade
    {
        ///1 - MODIFICAR A CLASSE (ENTIDADE) CONFORME O SEU TEMA
        ///1 - DESENVOLVER O CONSTRUTOR
        ///2 - DESENVOLVER AS CINCO PROPRIEDADES DA SUA ENTIDADE (SEU TEMA) SUBSTITUINDO PELOS FIELDS E MÉTODOS DE GETs/SETs JÁ EXISTENTE
        ///3 - DESENVOLVER PELO MENOS UM MÉTODO COM ALGUMA REGRA DE NEGÓCIO QUE REALIZE ALGUM TIPO DE CÁLCULO ENVOLVENDO A PROPRIEDADE DO TIPO DATETIME
            ///exemplos:
            ///data aniversario: quantidade de dias que faltam para o proximo aniversário do médico (em relação a data atual)
            ///data compra carro: há quantos anos o carro foi comprado (em relação a data atual)
            ///data de nomeação do prefeito: Quantidade de dias de gestão do prefeito até a data atual


        private Guid _id = Guid.NewGuid();
        private string _informacao2;
        private DateTime _informacao3;
        private bool _informacao4;
        private double _informacao5;

        public Guid GetId()
        {
            return _id;
        }

        public string GetInformacao2()
        {
            return _informacao2;
        }

        private void SetInformacao2(string value)
        {
            _informacao2 = value;
        }


        public DateTime GetInformacao3()
        {
            return _informacao3;
        }

        private void SetInformacao3(DateTime value)
        {
            _informacao3 = value;
        }


        public bool GetInformacao4()
        {
            return _informacao4;
        }

        private void SetInformacao4(bool value)
        {
            _informacao4 = value;
        }


        public double GetInformacao5()
        {
            return _informacao5;
        }

        private void SetInformacao5(double value)
        {
            _informacao5 = value;
        }

    }
}
