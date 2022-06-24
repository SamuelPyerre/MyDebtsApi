namespace MyDebtsApi.ViewModels
{
    public class ResultViewModel<T>
    {
        public ResultViewModel(T dados, List<string> erros)
        {
            Dados = dados;
            Erros = erros;
        }

        public ResultViewModel(T dados)
        {
            Dados = dados;
        }

        public ResultViewModel(List<string> erros)
        {
            Erros = erros;
        }

        public ResultViewModel(string erro)
        {
            Erros.Add(erro);
        }

        

        public T Dados { get; private set; }
        public List<string> Erros { get; private set; } = new();
    }
}