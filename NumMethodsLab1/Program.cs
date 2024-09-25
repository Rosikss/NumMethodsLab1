using System;

Console.WriteLine("Input epsilon:");
double epsilon = Convert.ToDouble(Console.ReadLine());

int maxIterations = 100;

Console.WriteLine("Relaxation method for the equation x^3 + 2x^2 - x - 2 = 0");
double x0Relaxation = -0.5;  
double tau = 0.159; 
RelaxationMethod(x0Relaxation, tau, epsilon, maxIterations);

Console.WriteLine(); 

Console.WriteLine("Modified Newton method for the equation x^3 - 2x^2 - x + 2 = 0");
double x0Newton = -1.1;  
ModifiedNewtonMethod(x0Newton, epsilon, maxIterations);
static double FuncRelaxation(double x)
{
    return Math.Pow(x, 3) + 2 * Math.Pow(x, 2) - x - 2;
}

static int CalculateIterationsRelaxation(double z0, double epsilon, double q)
{
    return (int)Math.Ceiling(Math.Log(Math.Abs(z0 / epsilon)) / Math.Log(1 / q));
}

static void RelaxationMethod(double x0, double tau, double epsilon, int maxIterations)
{
    double x = x0;
    int iteration = 0;

    double z0 = Math.Abs(x0 - 1); 
    double M1 = 11.75, m1 = 0.75; 
    double q = (M1 - m1) / (M1 + m1);

    int requiredIterations = CalculateIterationsRelaxation(z0, epsilon, q);

    Console.WriteLine($"A priori number of iterations (relaxation): {requiredIterations}");

    while (iteration < requiredIterations && iteration < maxIterations)
    {
        double fx = FuncRelaxation(x);
        double xNew = x - tau * fx;

        Console.WriteLine($"Iteration {iteration + 1}: x = {xNew}, f(x) = {fx}");

        if (Math.Abs(xNew - x) < epsilon)
        {
            Console.WriteLine($"Solution found using relaxation method: x = {xNew}, found in {iteration + 1} iterations.");
            return;
        }

        x = xNew;
        iteration++;
    }

    Console.WriteLine("Solution not found within the maximum number of iterations.");
}

static double FuncNewton(double x)
{
    return Math.Pow(x, 3) - 2 * Math.Pow(x, 2) - x + 2;
}

static double FuncNewtonDerivative(double x)
{
    return 3 * Math.Pow(x, 2) - 4 * x - 1;
}

static void ModifiedNewtonMethod(double x0, double epsilon, int maxIterations)
{
    double x = x0;
    double fDeriv0 = FuncNewtonDerivative(x0); 
    int iteration = 0;

    double z0 = Math.Abs(x0 - (-1)); 
    int requiredIterations = (int)Math.Ceiling(Math.Log(Math.Abs(z0 / epsilon)) / Math.Log(2)); 

    Console.WriteLine($"A priori number of iterations (Newton): {requiredIterations}");

    while (iteration < requiredIterations && iteration < maxIterations)
    {
        double fx = FuncNewton(x);
        double xNew = x - fx / fDeriv0;

        Console.WriteLine($"Iteration {iteration + 1}: x = {xNew}, f(x) = {fx}");

        if (Math.Abs(xNew - x) < epsilon)
        {
            Console.WriteLine($"Solution found using modified Newton method: x = {xNew}, found in {iteration + 1} iterations.");
            return;
        }

        x = xNew;
        iteration++;
    }

    Console.WriteLine("Solution not found within the maximum number of iterations.");
}