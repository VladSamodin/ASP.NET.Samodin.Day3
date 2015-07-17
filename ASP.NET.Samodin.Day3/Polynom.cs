using System;

namespace Task1.Polynom
{
    public sealed class Polynom : IEquatable<Polynom>, ICloneable
    {
        private readonly double[] coefficients;
        private readonly int degree;

        public int Degree
        {
            get
            {
                return degree;
            }
        }

        public Polynom(): this(0, 0) { }

        public Polynom(double coefficientMonomial, int degreeMonomial)
        {
            if (degreeMonomial < 0)
            {
                throw new ArgumentException("Degree monomial must be >= 0", "degreeMonomial");
            }
            if (coefficientMonomial == 0)
            {
                degreeMonomial = 0;
            }
            degree = degreeMonomial;
            coefficients = new double[degreeMonomial + 1];
            coefficients[degreeMonomial] = coefficientMonomial;
        }

        public Polynom(params double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException("coefficients");
            }
            if (coefficients.Length == 0)
            {
                throw new ArgumentException("Coefficient array is empty", "coefficients");
            }
            degree = DetermineDegreePolynom(coefficients);
            this.coefficients = new double[degree + 1];
            Array.Copy(coefficients, this.coefficients, this.coefficients.Length);
        }

        public Polynom(Polynom toCopy)
        {
            if (toCopy == null)
            {
                throw new ArgumentNullException("toCopy");
            }
            degree = toCopy.Degree;
            this.coefficients = toCopy.GetCoefficients();
        }

        public bool Equals(Polynom other)
        {
            if ((object)other == null)
            {
                return false;
            }
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }
            if (this.degree != other.degree)
            {
                return false;
            }
            bool isEqual = true;
            for (int i = 0; i <= this.degree; i++)
            {
                if (this[i] != other[i])
                {
                    isEqual = false;
                    break;
                }
            }
            return isEqual;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Polynom polynom = obj as Polynom;
            if ((object)polynom == null)
            {
                return false;
            }
            return this.Equals(polynom);
        }

        public object Clone()
        {
            //return new Polynom(this.coefficients);
            return this;
        }
        
        public override int GetHashCode()
        {
            int hash = 0;
            for (int i = 0; i <= Degree; i++)
            {
                byte[] data = BitConverter.GetBytes(this[i]);
                int x = BitConverter.ToInt32(data, 0);
                hash += x;
                hash -= (hash << 13) | (hash >> 19);
                int y = BitConverter.ToInt32(data, 4);
                hash += y;
                hash -= (hash << 13) | (hash >> 19);
            }
            return hash;
        }

        public double[] GetCoefficients()
        {
            return (double[])coefficients.Clone();
        }

        public double SubstituteVariableValue(double x)
        {
            double result = 0;
            double currentPowX = 1;
            for (int i = 0; i <= Degree; i++)
            {
                result += this[i] * currentPowX;
                currentPowX *= x;
            }
            return result;
        }

        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= coefficients.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                return coefficients[index];
            }
        }

        public static Polynom operator +(Polynom a, Polynom b)
        {
            return Add(a, b);
        }

        public static Polynom Add(Polynom a, Polynom b)
        {
            if (a.degree > b.degree)
            {
                SwapReferences(ref a, ref b);
            }
            int degeeResultPolynom = b.Degree;
            double[] resultPolynomCoefficients = new double[degeeResultPolynom + 1];
            for (int i = 0; i <= a.degree; i++)
            {
                resultPolynomCoefficients[i] = a[i] + b[i];
            }
            for (int i = a.degree + 1; i <= b.degree; i++)
            {
                resultPolynomCoefficients[i] = b[i];
            }
            return new Polynom(resultPolynomCoefficients);
        }

        public static Polynom operator -(Polynom a, Polynom b)
        {
            return Subtract(a, b);
        }

        public static Polynom Subtract(Polynom a, Polynom b)
        {
            return Add(a, Multiply(b, -1));
        }

        public static Polynom operator *(Polynom polynom, double factor)
        {
            return Multiply(polynom, factor);
        }

        public static Polynom Multiply(Polynom polynom, double factor)
        {
            if (factor == 0)
            {
                return new Polynom();
            }
            double[] resultPolynomCoefficients = new double[polynom.Degree + 1];
            for (int i = 0; i <= polynom.Degree; i++)
            {
                resultPolynomCoefficients[i] = factor * polynom[i];
            }
            return new Polynom(resultPolynomCoefficients);
        }

        public static bool operator ==(Polynom a, Polynom b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }
            if ((object)a == null || (object)b == null)
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(Polynom a, Polynom b)
        {
            return !(a == b);
        }
        
        private int DetermineDegreePolynom(double[] coefficients)
        {
            int degree = coefficients.Length - 1;
            for (; degree > 0; degree--)
            {
                if (coefficients[degree] != 0)
                    break;
            }
            return degree;
        }

        private static void SwapReferences(ref Polynom a, ref Polynom b)
        {
            Polynom bufferRefence = a;
            a = b;
            b = bufferRefence;
        }
        
    }
}
