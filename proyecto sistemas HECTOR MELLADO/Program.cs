static void JuegoDeBatalla(int[,] mapaEnemigo, int[] tamanosNaves, string[] nombresNaves)
{
    PosicionarNaves(mapaEnemigo, tamanosNaves, nombresNaves);
    int disparosRealizados = 0;
    int aciertosRestantes = 20;
    int puntos = 8000;
    do
    {
        Console.WriteLine("Enemigos en el océano:");
        for (int i = 0; i < nombresNaves.Length; i++)
        {
            Console.WriteLine("{0}: {1}", nombresNaves[i], tamanosNaves[i]);
        }
        Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        Console.WriteLine("Debe acertar un total de {0} disparos", aciertosRestantes);
        Console.WriteLine("Disparos realizados: " + disparosRealizados);
        Console.WriteLine("Disparos acertados: " + (20 - aciertosRestantes));
        Console.WriteLine("Puntaje: " + puntos);
        Console.WriteLine();
        MostrarTablero(mapaEnemigo);
        Console.WriteLine();
        try
        {
            Console.WriteLine("\nIngrese la posición a atacar (fila, columna)\n");
            Console.Write("> ");
            string[] posicion = Console.ReadLine().Split(',');
            int fila = int.Parse(posicion[0]) - 1;
            int columna = int.Parse(posicion[1]) - 1;
            if (mapaEnemigo[fila, columna] == 1 || mapaEnemigo[fila, columna] == 2 || mapaEnemigo[fila, columna] == 3)
            {
                mapaEnemigo[fila, columna] = 7;
                Console.Beep();
                Console.Clear();
                Console.WriteLine("¡Impacto! +150pts\n");
                puntos += 150;
                aciertosRestantes--;
                disparosRealizados++;
            }
            else if (mapaEnemigo[fila, columna] == 0)
            {
                mapaEnemigo[fila, columna] = 5;
                Console.Clear();
                Console.WriteLine("¡Fallaste! -250pts\n");
                puntos -= 250;
                disparosRealizados++;
            }
            else if (mapaEnemigo[fila, columna] == 7 || mapaEnemigo[fila, columna] == 5)
            {
                Console.Clear();
                Console.WriteLine("¡Ya disparaste aquí! -750pts");
                Console.WriteLine("¡Otra falla y te vas a conocer a San Pedro!");
                puntos -= 750;
                disparosRealizados++;
            }
        }
        catch (Exception)
        {
            Console.Clear();
            Console.WriteLine("Posición inválida\n");
            Console.WriteLine("Presione \"ENTER\" para continuar");
            Console.ReadKey();
        }
    } while (aciertosRestantes > 0 && puntos > 0);
    Console.WriteLine();
    Console.WriteLine("Presione enter para volver al menú principal");
    Console.ReadKey();
    Console.WriteLine();
}

static int ContarNaves(int[,] mapa, int idNave)
{
    int count = 0;
    for (int fila = 0; fila < mapa.GetLength(0); fila++)
    {
        for (int columna = 0; columna < mapa.GetLength(1); columna++)
        {
            if (mapa[fila, columna] == idNave)
            {
                count++;
            }
        }
    }
    return count;
}

static void MostrarTablero(int[,] mapa)
{
    Console.Write("   ");
    for (int col = 1; col <= mapa.GetLength(1); col++)
    {
        Console.Write($"{col,3}");
    }
    Console.WriteLine();
    Console.WriteLine();
    for (int fila = 1; fila <= mapa.GetLength(0); fila++)
    {
        Console.Write($"{fila,3}");
        for (int col = 1; col <= mapa.GetLength(1); col++)
        {
            if (mapa[fila - 1, col - 1] == 0)
            {
                Console.Write(string.Format("{0,3}", "-"));
            }
            else if (mapa[fila - 1, col - 1] < 4)
            {
                Console.Write(string.Format("{0,3}", "-"));
            }
            else if (mapa[fila - 1, col - 1] == 7)
            {
                Console.Write(string.Format("{0,3}", "X"));
            }
            else if (mapa[fila - 1, col - 1] == 5)
            {
                Console.Write(string.Format("{0,3}", "O"));
            }
        }
        Console.WriteLine();
    }
}

static void PosicionarNaves(int[,] mapa, int[] tamanosNaves, string[] nombresNaves)
{
    Random aleatorio = new Random();
    for (int x = 0; x < mapa.GetLength(0); x++)
    {
        for (int y = 0; y < mapa.GetLength(1); y++)
        {
            mapa[x, y] = 0;
        }
    }
    for (int i = 0; i < tamanosNaves.Length; i++)
    {
        int cantidadNaves = tamanosNaves[i];
        for (int j = 0; j < cantidadNaves; j++)
        {
            int filaInicial, columnaInicial, direccion;
            bool posicionValida;
            do
            {
                posicionValida = true;
                filaInicial = aleatorio.Next(0, mapa.GetLength(0));
                columnaInicial = aleatorio.Next(0, mapa.GetLength(1));
                direccion = aleatorio.Next(2);
                if ((direccion == 0 && columnaInicial + tamanosNaves[i] > mapa.GetLength(1)) ||
                    (direccion == 1 && filaInicial + tamanosNaves[i] > mapa.GetLength(0)))
                {
                    posicionValida = false;
                    continue;
                }
                for (int k = 0; k < tamanosNaves[i]; k++)
                {
                    if (direccion == 0 && mapa[filaInicial, columnaInicial + k] != 0 ||
                        direccion == 1 && mapa[filaInicial + k, columnaInicial] != 0)
                    {
                        posicionValida = false;
                        break;
                    }
                }
            } while (!posicionValida);
            for (int k = 0; k < tamanosNaves[i]; k++)
            {
                if (direccion == 0)
                {
                    mapa[filaInicial, columnaInicial + k] = i + 1;
                }
                else
                {
                    mapa[filaInicial + k, columnaInicial] = i + 1;
                }
            }
        }
    }
}
int opcion = 3;
do
{
    try
    {
        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        Console.WriteLine("¡Bienvenido al Battleship mas grandioso, exitoso, fenomenal, guapo y fornido del sistema solar!");
        Console.WriteLine();
        Console.WriteLine("Opción 1: Jugar");
        Console.WriteLine();
        Console.WriteLine("Opción 2: Recargar");
        Console.WriteLine();
        Console.WriteLine("Opción 3: Salir");
        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        opcion = Convert.ToInt32(Console.ReadLine());
        switch (opcion)
        {
            case 1:
                int[,] mapaEnemigo = new int[20, 20];
                int[] tamanosNaves = { 2, 3, 4 };
                string[] nombresNaves = { "Barco", "Navío", "Portaaviones" };
                JuegoDeBatalla(mapaEnemigo, tamanosNaves, nombresNaves);
                break;
        }
    }
    catch (Exception)
    {
        Console.Clear();
        Console.WriteLine("Tecla inválida\n");
        Console.WriteLine("Presione \"ENTER\" para continuar");
        Console.ReadKey();
    }
} while (opcion != 3);
{
    Console.WriteLine("¡Gracias por jugar! Nos vemos la próxima.");
}
