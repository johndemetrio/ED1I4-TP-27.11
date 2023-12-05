using TPFinal;

Locacao atividadeFinal = new Locacao();
int opcao = 0;

static void apagaTela()
{
    Console.WriteLine("Aperte qualquer tecla para continuar...");
    Console.ReadKey();
    Console.Clear();
}


do
{

    Console.WriteLine("1. Cadastrar tipo de equipamento\r\n2. Consultar tipo de equipamento (com os respectivos itens cadastrados)\r\n3. Cadastrar equipamento (item em um determinado tipo)\r\n4. Registrar Contrato de Locação\r\n5. Consultar Contratos de Locação (com os respectivos itens contratados)\r\n6. Liberar de Contrato de Locação\r\n7. Consultar Contratos de Locação liberados (com os respectivos itens)\r\n8. Devolver equipamentos de Contrato de Locação liberado ");
    try
    {
        opcao = int.Parse(Console.ReadLine());
    }
    catch
    {
        opcao = 999;
    }


    switch (opcao)
    {
        case 0:
            break;

        case 1:
            string nomec1;
            Console.WriteLine("Qual o nome do tipo de equipamento?");
            nomec1 = Console.ReadLine();

			atividadeFinal.cadastraTipoEquipamento(nomec1, atividadeFinal.verificaId(atividadeFinal.Estoques));
            Console.WriteLine("Cadastrado com sucesso!");
            apagaTela();

            break;

        case 2:
            Console.WriteLine(atividadeFinal.consultaTipoEquipamento());

            apagaTela();

            break;

        case 3:
            string nomec2;
            double vlrc2;
            int idc2;

            Console.WriteLine("Qual o nome do equipamento?");
            nomec2 = Console.ReadLine();
            Console.WriteLine("Qual o valor diário do equipamento?");
            vlrc2 = double.Parse(Console.ReadLine());
            Console.WriteLine("-------------------------------------");
            Console.WriteLine(atividadeFinal.consultaTipoEquipamento());
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Qual o id do tipo de item do equipamento?");
            idc2 = int.Parse(Console.ReadLine());

            if (atividadeFinal.cadastraEquipamento(atividadeFinal.achaTipoItem(idc2), nomec2, vlrc2))
            {
                Console.WriteLine("Cadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine("Não foi possível cadastrar pois o ID é inválido");
            }


            apagaTela();

            break;

        case 4:

            int opcaoc4 = -1;
            int idc4, qtdc4;
            List<Itens_contrato> itensc4 = new List<Itens_contrato>();
            DateTime retornoc4, saidac4;

            Console.WriteLine("Qual a data de saída dos equipamentos?");
            saidac4 = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Qual a data de retorno dos equipamentos?");
            retornoc4 = DateTime.Parse(Console.ReadLine());

            do
            {
                Console.WriteLine("Qual o ID do tipo de equipamento desejado?");
                idc4 = int.Parse(Console.ReadLine());

                Tipo_item testec4 = atividadeFinal.achaTipoItem(idc4);

                if (testec4.Id != -1)
                {
                    Console.WriteLine("Qual a quantidade desejada?");
                    qtdc4 = int.Parse(Console.ReadLine());

                    if (qtdc4 <= 0)
                    {
                        Console.WriteLine("Quantidade com numeração incorreta.");
                    }
                    else
                    {
                        Itens_contrato novoItem = new Itens_contrato(testec4, qtdc4);
                        itensc4.Add(novoItem);

                        Console.WriteLine("Adicionado!");
                        Console.WriteLine("Deseja adicionar mais algum item ao contrato? (0- Não / 1- Sim)");
                        opcaoc4 = int.Parse(Console.ReadLine());
                    }
                }
                else
                {
                    Console.WriteLine("ID inexistente. Segue lista de IDs.");
                    Console.WriteLine(atividadeFinal.consultaTipoEquipamento());
                }

            }
            while (opcaoc4 != 0);

            if (atividadeFinal.verificaItensContrato(itensc4))
            {
				atividadeFinal.registraContrato(atividadeFinal.verificaId(atividadeFinal.Contratos), saidac4, retornoc4, itensc4);
                Console.WriteLine("Contrato adicionado!");
            }
            else
            {
                Console.WriteLine("Quantidades de itens indisponíveis. Contrato impossibilitado.");
            }

            apagaTela();

            break;

        case 5:
            Console.WriteLine(atividadeFinal.consultaContrato());

            apagaTela();

            break;

        case 6:
            int idc6;

            Console.WriteLine("Qual o ID do contrato?");
            idc6 = int.Parse(Console.ReadLine());

            if (atividadeFinal.liberaContrato(idc6))
            {
                Console.WriteLine("Contrato liberado!");
            }
            else
            {
                Console.WriteLine("Contrato não liberado");
            }

            apagaTela();
            break;

        case 7:
            Console.WriteLine(atividadeFinal.consultaContratoLiberado());

            apagaTela();
            break;

        case 8:
            int idc8;
            int opcaoc8 = -1;
            Contrato c8 = null;

            Console.WriteLine("Qual o Id do contrato?");
            idc8= int.Parse(Console.ReadLine());

            Console.WriteLine(atividadeFinal.devolveItens(idc8));

            foreach (Contrato c in atividadeFinal.Contratos)
            {
                if (c.Id == idc8)
                {
                    c8 = c;
                    break;
                }
            }

            if (c8 != null)
            {
                for (int i = 0; i < c8.ListaContrato.Count; i++)
                {
                    List<Item> itensToRemove = new List<Item>();

                    for (int j = 0; j < c8.ListaContrato[i].ItemContrato.Itens.Count; j++)
                    {
                        Console.WriteLine("O item " + c8.ListaContrato[i].ItemContrato.Itens[j].Nome + " foi avariado? (0- Não / 1- Sim)");
                        opcaoc8 = int.Parse(Console.ReadLine());

                        if (opcaoc8 == 1)
                        {
                            itensToRemove.Add(c8.ListaContrato[i].ItemContrato.Itens[j]);
                        }
                    }

                    foreach (Item itemToRemove in itensToRemove)
                    {
						atividadeFinal.realizaDevolucao(idc8, opcaoc8, i, c8.ListaContrato[i].ItemContrato.Itens.IndexOf(itemToRemove));
                    }
                }

				atividadeFinal.Contratos.Remove(c8);
            }



            apagaTela();
            break;



        default:
            Console.WriteLine("Opção indesejada. Redigite.");
            apagaTela();
            break;
    }
}
while (opcao != 0);