namespace APIGestaoVendas.Service
{
    public static class AcessoServico
    {
        public static string ConverterIdEmString(int codigoRegiao)
        {
            var nomeRegiao = "";
            switch (codigoRegiao)
            {
                case 1:
                    nomeRegiao = "Norte";
                    break;
                case 2:
                    nomeRegiao = "Nordeste";
                    break;
                case 3:
                    nomeRegiao = "Sudeste";
                    break;
                case 4:
                    nomeRegiao = "Sul";
                    break;
                case 5:
                    nomeRegiao = "CentroOeste";
                    break;
            }
            return nomeRegiao;
        }
    }
}
