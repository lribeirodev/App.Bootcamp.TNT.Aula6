using System;
using System.Collections;

/*
    ATIVIDADE: Fazer um sistema de escola que leia o nome do aluno e as notas dos 4 bimestres e a 
    quantidade total de faltas, calcular a media geral do ano e  decidir baseado nas regras abaixo 
    se o aluno passou, reprovou ou ficou de de recuperacao:

    Se a nota do aluno for maior que 7  aluno aprovado.
    Se nota menor que 5 reprovado.
    Se nota entre 5 e 7 aluno em recuperação.

    Obs: se quantidade de faltas for maior ou igual a  50  aluno também esta  de recuperação.
*/
class Program
{
    public static void Main(string[] args)
    {
        EscolaSimples.Run();
        //EscolaAvancada.Run();
    }
}

class EscolaSimples
{

    public static void Run()
    {
        Console.WriteLine("Digite nome do aluno?");
        var aluno = Console.ReadLine();
        Double[] notas = new Double[4];
        int[] faltas = new int[4];

        for (int i = 0; i < notas.Length; i++)
        {
            Console.WriteLine($"Bimestre {i + 1}");
            Console.WriteLine($"Nota: ");
            var resposta = Console.ReadLine();
            Double nota;
            while (!Double.TryParse(resposta, out nota))
            {
                Console.WriteLine($"Valor Inválido Digite Novamente o Total do Bimestre {i + 1}");
                resposta = Console.ReadLine();
            }
            notas[i] = nota;

            Console.WriteLine($"Faltas: ");
            var resposta2 = Console.ReadLine();
            int falta;
            while (!int.TryParse(resposta2, out falta))
            {
                Console.WriteLine($"Valor Inválido Digite Novamente a Falta Total do Bimestre {i + 1}");
                resposta2 = Console.ReadLine();
            }
            faltas[i] = falta;
            Console.WriteLine("\n");
        }

        Double somaTotalNotas = 0;
        foreach (Double n in notas)
        {
            somaTotalNotas += n;
        }
        Double media = somaTotalNotas / notas.Length;

        int somaTotalFaltas = 0;
        foreach (int n in faltas)
        {
            somaTotalFaltas += n;
        }

        string result;
        switch (somaTotalNotas)
        {
            case > 7: result = "aprovado"; break;
            case < 5: result = "reprovado"; break;
            default: result = "recuperação"; break;
        }

        result = somaTotalFaltas > 50 ? "reprovado por falta" : result;

        Console.WriteLine($"Resultado: {result}");
    }

}

class EscolaAvancada
{

    class Aluno
    {
        public string? Nome { get; set; }
        public double[]? Notas { get; set; }
        public int[]? Faltas { get; set; }
    }

    public static void Run()
    {

        String[] dados = {
                "Nome:Lucas Ribeiro;Notas:{5.5,7.5,8.5,9.5};Faltas:{10,7,8,10}",
                "Nome:Laura;Notas:{10,7,5,2,10};Faltas:{5,15,10,7,10}",
                "Nome:Gabriella;Notas:{10,7,8,7,10};Faltas:{0,15,10,7,10}",
                "Nome:Heitor;Notas:{8,7,5,2,10};Faltas:{5,15,10,7,10}",
                };

        Aluno[] alunos = criarObjetosAluno(dados);

       imprimirResultado(alunos);
       
    }

    private static Aluno[] criarObjetosAluno(String[] dados)
    {
        
        Aluno[] alunos = new Aluno[dados.Length];
        int cont = 0;

        for(int i = 0; i < dados.Length; i++)
        {
            Aluno a = new Aluno();
            String[] itemDados = dados[i].Split(";");
            for(int j = 0; j < itemDados.Length; j++)
            {
                String[] campo = itemDados[j].Split(":");
                for(int u = 0; u < campo.Length ; u++)
                {
                    string field = campo[0];
                    string value = campo[1];

                    if(field.Equals("Nome"))
                    {
                        a.Nome = value;
                    }
                    else if(field.Equals("Notas"))
                    {
                        a.Notas = ConversaoArray<Double>(value);
                    }
                    else if(field.Equals("Faltas"))
                    {
                        a.Faltas = ConversaoArray<int>(value);
                    }
                }
                
            }

            alunos[cont] = a;
            cont++;
        }        

        return alunos;
    }

    private static T[] ConversaoArray<T>(string dados)
    {
        Type tipo = typeof(T);

        dados = dados.Substring(1, dados.Length - 2);
        string[] array = dados.Split(',');
        T[] objArray = new T[array.Length];

        int cont = 0;
        foreach (string i in array)
        {
            T obj = (T)Convert.ChangeType(i, tipo);
            objArray[cont] = obj;
            cont++;
        }

        return objArray;
    }

    private static void imprimirResultado(Aluno[] alunos)
    {

        foreach (Aluno aluno in alunos)
        {
            // soma das notas
            double media = calcularMedia(aluno.Notas!);
            int faltas = calcularFaltas(aluno.Faltas!);
            Console.WriteLine($"Aluno: {aluno.Nome} Media: {media} Faltas: {faltas} Status: {verificarAprovacao(media,faltas)}");
        }

    }

    private static double calcularMedia(double[] notas)
    {
        double somaDasNotas = 0;
        foreach (double item in notas)
        {
            somaDasNotas += Math.Abs(item);
        }
        return (somaDasNotas / notas.Length) > 10 ? Math.Floor((somaDasNotas / notas.Length)) : (somaDasNotas / notas.Length);
    }

    private static int calcularFaltas(int[] faltas)
    {
        int somaDasFaltas = 0;
        foreach (int item in faltas)
        {
            somaDasFaltas += item;
        }

        return somaDasFaltas;
    }

    private static String verificarAprovacao(double media, int faltas)
    {

        if (media > 7 && faltas < 50)
        {
            return "Aprovado";
        }
        else if (media < 5 && faltas < 50)
        {
            return "Reprovado";
        }
        else
        {
            return "Recuperação";
        }
    }
}