using System;
using System.Drawing;
class CleanTemp
{
    enum Opcao { Executar = 1, Sair = 2, Info = 3 }
    public class Global
    {
        public static string Temp2 = Path.GetTempPath();
    }

    public static void Main(string[] args)
    {
        bool executando = false;
        while (!executando)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("LIMPADOR DE PASTAS TEMPORARIAS!");
            Console.ResetColor();
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1-Executar\n2-Sair");
            Opcao index = (Opcao)int.Parse(Console.ReadLine());
            Console.Clear();

            switch (index)
            {
                case Opcao.Executar:
                    {
                        Console.WriteLine($"Essa opção ira limpar as seguintes pastas:\n{Global.Temp2}");
                        Console.WriteLine("Deseja continuar?:\n1-Sim\n2-Nao");
                        int resposta = int.Parse(Console.ReadLine());
                        Console.Clear();
                        if (resposta == 1)
                        {
                            Temp();
                        }
                        else
                        {
                            Console.Clear();
                            continue;
                        }
                        break;
                    }

                case Opcao.Sair:
                    {
                        executando = true;
                        break;
                    }

            }
        }
    }
    public static void Temp()
    {
        int pasta_apagado = 0;
        int arquivo_apagado = 0;
        int arquivo_aberto = 0;
        int pasta_aberto = 0;
        string[] pastas = Directory.GetDirectories(Global.Temp2);
        string[] arquivos = Directory.GetFiles(Global.Temp2);

        foreach (string arquivo in arquivos)
        {
            try
            {
                File.SetAttributes(arquivo, FileAttributes.Normal);
                File.Delete(arquivo);
                arquivo_apagado++;
            }
            catch
            {
                arquivo_aberto++;
                continue;
            }
            Console.WriteLine($"Deletando {arquivo}...");
        }

        foreach (string pasta in pastas)
        {
            try
            {
                Directory.Delete(pasta, true);
                Console.WriteLine($"Deletando {pasta}...");
                pasta_apagado++;
            }
            catch
            {
                pasta_aberto++;
                continue;
            }
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{arquivo_apagado} arquivos foram apagados");
        Console.WriteLine($"{pasta_apagado} pastas foram apagados");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{arquivo_aberto + pasta_aberto} arquivos estavam abertos");
        Console.ResetColor();
        Console.WriteLine("Ação Concluida! Aperte ENTER para continuar");
        Console.ReadLine();
        Console.Clear();
    }
}
