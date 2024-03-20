namespace HomeWork2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Список операторов: \n" +
                "+ сложение\n" +
                "- вычитание\n" +
                "* умножение\n" +
                "/ деление\n" +
                "% процент от числа\n" +
                "√ квадратный корень (унарный)\n");

            while (true)
            {
                Console.Write("Введите выражение (число оператор число, либо оператор число): ");
                string input = Console.ReadLine();

                int operatorIndex = -1;
                char[] operators = ['+', '-', '*', '/', '√', '%'];
                for (int i = 0; i < operators.Length; i++)
                {        
                    bool lastIteration = i == operators.Length - 1;
                    operatorIndex = input.LastIndexOf(operators[i]);
                    if (operatorIndex != -1 && operators[i] != '-')
                        break;
                    else if (lastIteration)
                        operatorIndex = input.LastIndexOf('-');
                }                

                if (operatorIndex == -1)
                {
                    ShowErrorMessage("оператор не найден!");
                    continue;
                }

                string operatorInString = input.Substring(operatorIndex, 1);

                if (operatorInString == "√")
                {
                    string numberInString = input.Substring(operatorIndex + 1);

                    try
                    {
                        double number = double.Parse(numberInString);

                        if (number < 0)
                        {
                            ShowErrorMessage("нельзя извлечь квадратный корень из отрицательного числа!");
                            continue;
                        }    

                        double result = Math.Sqrt(number);

                        ShowResult(result);
                    }
                    catch (FormatException)
                    {
                        ShowErrorMessage("некорректный ввод чисел!");
                    }
                    catch (Exception e)
                    {
                        ShowErrorMessage(e.Message);
                    }
                }
                else
                {
                    string firstNumberInString = input.Substring(0, operatorIndex);
                    string secondNumberInString = input.Substring(operatorIndex + 1);

                    try
                    {
                        if (firstNumberInString[firstNumberInString.Length - 1] == '-')
                        {
                            firstNumberInString = firstNumberInString.Substring(0, firstNumberInString.Length - 1);
                            secondNumberInString = "-" + secondNumberInString;
                        }

                        double firstNumber = double.Parse(firstNumberInString);
                        double secondNumber = double.Parse(secondNumberInString);

                        double result;

                        switch (operatorInString)
                        {
                            case "+":
                                result = firstNumber + secondNumber;
                                break;
                            case "-":
                                result = firstNumber - secondNumber;
                                break;
                            case "*":
                                result = firstNumber * secondNumber;
                                break;
                            case "/":
                                if (secondNumber == 0)
                                {
                                    ShowErrorMessage("на 0 делить нельзя!");
                                    continue;
                                }
                                result = firstNumber / secondNumber;
                                break;
                            case "%":
                                result = secondNumber * firstNumber/100;
                                break;
                            default:
                                ShowErrorMessage("неверный оператор!");
                                continue;
                        }

                        ShowResult(result);
                    }
                    catch (FormatException)
                    {
                        ShowErrorMessage("некорректный ввод чисел!");
                    }
                    catch (Exception e)
                    {
                        ShowErrorMessage(e.Message);
                    }
                }


                Console.Write("Нажмите ESC для выхода или любую другую клавишу, чтобы продолжить");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    break;

                Console.WriteLine();
            }
        }

        static void ShowErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка: " + message);
            Console.ResetColor();
        }

        static void ShowResult(double result)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Результат: " + result);
            Console.ResetColor();
        }
    }
}
