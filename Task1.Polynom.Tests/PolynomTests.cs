using System;
using Task1.Polynom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Task1.Polynom.Tests
{
    [TestClass]
    public class PolynomTests
    {
        [TestMethod]
        public void Test_GetCoefficients()
        {
            double[] coefficients = new double[] { 1, 2, 3, 4, 5 };
            Polynom polynom = new Polynom(coefficients);

            IStructuralEquatable coeff = coefficients;
            Assert.IsTrue(coeff.Equals(polynom.GetCoefficients(), StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Constructor_WithoutParameters_0()
        {
            Polynom polynom = new Polynom();

            double[] expected = new double[] { 0 };

            IStructuralEquatable actual = polynom.GetCoefficients();
            Assert.IsTrue(actual.Equals(expected, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Constructor_Coefficietn2Degree5_MonomialDgree5()
        {
            Polynom polynom = new Polynom(2, 5);

            double[] expected = new double[] { 0, 0, 0, 0, 0, 2 };

            IStructuralEquatable actual = polynom.GetCoefficients();
            Assert.IsTrue(actual.Equals(expected, StructuralComparisons.StructuralEqualityComparer));
        }

         [TestMethod]
        public void SubstituteVariableValue()
        {
             double[] coefficients = new double[] { 1, 2, 3, 4, 5 };
             double x = 2;
             Polynom polynom = new Polynom(coefficients);

             double expected = 129;
             double actual = polynom.SubstituteVariableValue(x);

             Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Equals_EquivalentPolynomials_True()
        {
            double[] coefficients = new double[] { 1, 2, 3, 4, 5 };
            Polynom a = new Polynom(coefficients);
            Polynom b = new Polynom(coefficients);

            Assert.IsTrue(a.Equals(b));
        }

        public void Equals_InequivalentPolynomials_False()
        {
            double[] coefficientsA = new double[] { 1, 2, 3, 4, 5 };
            double[] coefficientsB = new double[] { 1, 2, 3, 3, 5 };
            Polynom a = new Polynom(coefficientsA);
            Polynom b = new Polynom(coefficientsB);

            Assert.IsFalse(a.Equals(b));
        }

        public void Equals_PolynomAndNull_False()
        {
            Polynom a = new Polynom();
            Polynom b = null;

            Assert.IsFalse(a.Equals(b));
        }

        
        public void OperatorEquality_NullAndPolynom_False()
        {
            Polynom a = null;
            Polynom b = new Polynom();

            Assert.IsFalse(a == b);
        }

        public void OperatorEquality_NullAndNull_True()
        {
            Polynom a = null;
            Polynom b = null;

            Assert.IsTrue(a == b);
        }

        [TestMethod]
        public void Test_Clone()
        {
            double[] coefficients = new double[] { 1, 2, 3, 4, 5 };
            Polynom a = new Polynom(coefficients);
            Polynom b = (Polynom)a.Clone();

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Add_PolynomsTheSameDegree_PolynomTheSameDegree()
        {
            double[] coefficientsA = new double[] { 1, 2, 3, 4, 5 };
            double[] coefficientsB = new double[] { 4, 1, 8, 3, 5 };
            Polynom a = new Polynom(coefficientsA);
            Polynom b = new Polynom(coefficientsB);

            double[] expected = new double[] { 5, 3, 11, 7, 10 };
            IStructuralEquatable actual = (a + b).GetCoefficients();

            Assert.IsTrue(actual.Equals(expected, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Add_PolynomsDifferentDegrees_PolynomMaxDegree()
        {
            double[] coefficientsA = new double[] { 1, 2, 3, 4 };
            double[] coefficientsB = new double[] { 4, 1, 8, 3, 5 };
            Polynom a = new Polynom(coefficientsA);
            Polynom b = new Polynom(coefficientsB);

            double[] expected = new double[] { 5, 3, 11, 7, 5 };
            IStructuralEquatable actual = (a + b).GetCoefficients();

            Assert.IsTrue(actual.Equals(expected, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Subtract_PolynomsTheSameDegree_PolynomTheSameDegree()
        {
            double[] coefficientsA = new double[] { 1, 2, 3, 4, 6 };
            double[] coefficientsB = new double[] { 4, 1, 8, 3, 5 };
            Polynom a = new Polynom(coefficientsA);
            Polynom b = new Polynom(coefficientsB);

            double[] expected = new double[] { -3, 1, -5, 1, 1 };
            IStructuralEquatable actual = (a - b).GetCoefficients();

            Assert.IsTrue(actual.Equals(expected, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Subtract_PolynomsDifferentDegrees_PolynomMaxDegree()
        {
            double[] coefficientsA = new double[] { 1, 2, 3, 4 };
            double[] coefficientsB = new double[] { 4, 1, 8, 3, 5 };
            Polynom a = new Polynom(coefficientsA);
            Polynom b = new Polynom(coefficientsB);

            double[] expected = new double[] { -3, 1, -5, 1, -5 };
            IStructuralEquatable actual = (a - b).GetCoefficients();

            Assert.IsTrue(actual.Equals(expected, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Subtract_PolynomsWithEqualCoefficientMaxDegree_PolynomLessDegree()
        {
            double[] coefficientsA = new double[] { 1, 2, 3, 4, 5 };
            double[] coefficientsB = new double[] { 4, 1, 8, 3, 5 };
            Polynom a = new Polynom(coefficientsA);
            Polynom b = new Polynom(coefficientsB);

            double[] expected = new double[] { -3, 1, -5, 1 };
            IStructuralEquatable actual = (a - b).GetCoefficients();

            Assert.IsTrue(actual.Equals(expected, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Multiply_PolynomBy2_DoubledCoefficients()
        {
            double[] coefficients = new double[] { 1, 2, 3, 4, 5 };
            Polynom polynom = new Polynom(coefficients);

            double[] expected = new double[] { 2, 4, 6, 8, 10 };
            IStructuralEquatable actual = (polynom * 2).GetCoefficients();

            Assert.IsTrue(actual.Equals(expected, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Multiply_PolynomByMinus1_InvertedSignsCoefficients()
        {
            double[] coefficients = new double[] { 1, 2, 3, 4, 5 };
            Polynom polynom = new Polynom(coefficients);

            double[] expected = new double[] { -1, -2, -3, -4, -5 };
            IStructuralEquatable actual = (polynom * (-1)).GetCoefficients();

            Assert.IsTrue(actual.Equals(expected, StructuralComparisons.StructuralEqualityComparer));
        }
    }
}
