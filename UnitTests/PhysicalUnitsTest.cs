using System;
using System.Collections.Generic;
using System.Linq;
using PhysicalQuantities;
using Xunit;
using System.Reflection;
using PhysicalQuantities;

namespace UnitTests
{
    /// <summary>
    ///     Summary description for PhysicalUnitsTest
    /// </summary>
    public class PhysicalUnitsTest
    {
        [Fact]
        public void TestLength1()
        {
            var l1 = new Length(100, Unit.SI_Prefix.milli);

            var l2 = new Length(1);

            var l3 = l1 + l2;

            Assert.True(l3.Unit!.Equals(Units.Metre));
        }

        [Fact]
        public void TestLength2()
        {
            var l1 = new Length(100, Unit.SI_Prefix.milli);

            var l2 = new Length(1);

            var l4 = l2 + l1;

            l4.SetPrefix(Unit.SI_Prefix.milli);

            Assert.True(l4.PrefixIndex == Unit.SI_Prefix.milli);

        }

        [Fact]
        public void TestLength3()
        {
            var l1 = new Length(100, Unit.SI_Prefix.milli);

            var l2 = new Length(1);

            var l5 = l2 - l1;

            var l6 = l1 - l2;

            Assert.True(l5.Equals(-l6));

        }

        [Fact]
        public void TestLength5()
        {
            var l1 = new Length(100, Units.Lengths.Centimetre);

            var l2 = new Length(1);

            var l5 = l2 + l1;



            Assert.True(l5.Unit!.Equals(Units.Metre));

        }

        [Fact]
        public void TestLength6()
        {
            var l1 = new Length(1000, Units.Lengths.Millimetre);

            var l2 = new Length(1);

            var l5 = l2 + l1;

            Assert.True(l5.Unit!.Equals(Units.Metre));

            var l6 = l5.ConvertToUnit(Units.Lengths.Kilometre);

            string text = $"l6 = {l6}";

        }

        [Fact]
        public void TestVolume1()
        {
            var l1 = new Length(100, Unit.SI_Prefix.milli);

            var l2 = new Length(1);

            var l3 = l1 + l2;

            var l4 = l2 + l1;

            Volume v1 = l1 * l2 * l3;
            
            Assert.True(v1.Unit!.Equals(Units.QubicMetre));

            Assert.Throws<IncompatibleUnits>(() =>
                {
                    var tests = v1.ConvertToDerivedUnit();
                    return tests;
                }
            );
        }


        [Fact]
        public void TestVolume2()
        {
            Volume v1 = new Volume(100);

            v1.SetUnit(Units.HourEquivalent);
        }

        [Fact]
        public void TestLength4()
        {
            var l1 = new Length(100, Unit.SI_Prefix.milli);

            var l2 = new Length(1);

            DimensionlessQuantity r = l2 / l1;

            Assert.True(r.Unit!.SameDimension(Units.Dimensionless));
        }

        [Fact]
        public void TestArea()
        {
            var l1 = new Length(100, Unit.SI_Prefix.milli);

            var l2 = new Length(1); // meter

            var a = l1 * l2;

            Assert.True(a.Value.Equals(0.1));
            Assert.True(a.Unit!.Equals(Units.SquareMetre));

        }

        [Fact]
        public void TestTime1()
        {
            var t1 = new Time(1000, Units.Second, Unit.SI_Prefix.milli);
            var t2 = new Time(2000, Units.Second, Unit.SI_Prefix.milli);

            var t3 = t1 + t2;
            var t4 = t1 - t2;

            Assert.True(t3 > t4);

        }

        [Fact]
        public void TestEinstein()
        {
            Mass m = new Mass(90);

            var e = (m * QuantityBase.Pow(Constants.SpeedOfLight, 2)).AdjustPrefix();

            double expected = 90.0 * 299792458.0 * 299792458.0; // E = mc²
            Assert.True(Math.Abs(e.ValueInSIUnits - expected) < 1e9); // within 1 GJ tolerance

            Assert.True(e.Unit!.SameDimension(Units.Joule));
        }


        [Fact]
        public void TestAcceleration()
        {
            var t1 = new Time(1000, Units.Second, Unit.SI_Prefix.milli);
            var t2 = new Time(2000, Units.Second, Unit.SI_Prefix.milli);

            var r = t1 * t2;

            var l1 = new Length(100, Units.Metre, Unit.SI_Prefix.milli);

            Acceleration? a = null;
            try
            {
                a = l1 / r;
            }
            catch (IncompatibleUnits)
            {
                Console.Out.Write("Mäh");
            }

            Assert.True(a is Acceleration);

        }

        [Fact]
        public void TestSpeed1()
        {
            var t1 = new Time(1, Units.Second);
            var l1 = new Length(100, Units.Metre);
            
            Speed s1 = l1 / t1;

            s1.SetUnit(Units.Mph);


            // should be 
            Assert.Equal(223.693629, s1.Value, 6);
        }

        [Fact]
        public void TestSpeed2()
        {
            var t1 = new Time(1000, Units.Second, Unit.SI_Prefix.milli);
            var t2 = new Time(2000, Units.Second, Unit.SI_Prefix.milli);
            var l1 = new Length(100, Units.Metre, Unit.SI_Prefix.milli);

            Speed s1 = l1 / t1;
            Speed s2 = l1 / t2;

            var s3 = s1 + s2;

            var s = s3.ToString();
        }

        [Fact]
        public void TestVolume()
        {
            var v1 = new Volume(2, Units.HourEquivalent);
            var v2 = new Volume(1000, Unit.SI_Prefix.hekto);
            var v3 = v1 + v2;
        }

        [Fact]
        public void TestPowerOf()
        {
            Length l = new Length(10, Unit.SI_Prefix.centi);
            
            Volume v = QuantityBase.Pow(l, 3).ToUnit(Units.Litre);
            Assert.True(v != null);
        }

        [Fact]
        public void TestGravity()
        {
            var m = new Mass(100);

            var v = new Volume(1000);

            Density? d = null;

            try
            {
                d = m / v;
            }
            catch (IncompatibleUnits)
            {
                Console.Out.WriteLine("test");
            }

            var g = new Acceleration(9.82);

            Force f = m * g;
            
            Console.Out.Write("f = " + f);
        }

        [Fact]
        public void TestTemperature()
        {
            var t1 = new Temperature(0, Units.Celsius);
            var t2 = new Temperature(32, Units.Farenheit);
            var t3 = t1 + t2; // gives 273,15 degrees Celsius!

            Assert.Equal(273.15, t3.Value, 2);
        }

        [Fact]
        public void TestVolume7()
        {
            var l1 = new Length(3);
            var l2 = new Length(4);
            var l3 = new Length(5);

            Volume v1 = QuantityBase.Pow(l1, 3);

            Volume v2 = l1 * l2 * l3;

            var vdiff = v1 - v2;

            Area a = v2 / l1;
        }

        [Fact]
        public void TestTime()
        {
            var t1 = new Time(30, Units.Minute);

            var t2 = new Time(3, Units.Hour);

            var t3 = new Time(120);

            var t4 = t1 + t2 + t3;

            var multiplier = new DimensionlessQuantity(5);

            Time t5 = multiplier * t1;

            t5.SetUnit(t1.Unit);
        }

        [Fact]
        public void TestPotentialKineticEnergy()
        {
            Energy kinetic = (new Mass(10)) * QuantityBase.Pow(new Speed(10), 2) / (new DimensionlessQuantity(2));

            Energy? potential = null;

            var m2 = new Mass(91);
            var gravity = new Acceleration(9.82);
            var heightAboveEarth = new Length(100);

            try
            {
                potential = m2 * gravity * heightAboveEarth;
            }
            catch (IncompatibleUnits e)
            {
                Console.Out.WriteLine(e.Message);
            }

            //  potential.SetUnit(Units.Joule);
            potential!.SetPrefix(Unit.SI_Prefix.kilo);
        }

        [Fact]
        public void TestOhmsLaw()
        {
            //trying ohms famous law...
            var R = new Resistance(1);
            var I = new Current(5);

            Voltage U = R * I;

            Assert.Equal(5,U.Value,2);
            Assert.True(U.Unit!.Equals(Units.Volt));
        }

        [Fact]
        public void TestPower1()
        {
            var U = new Voltage(20, Unit.SI_Prefix.milli);
            var I = new Current(5, Unit.SI_Prefix.milli);

            Power P = U * I;

            P.SetPrefix(Unit.SI_Prefix.mikro);
        }

        [Fact]
        public void TestPower2()
        {
            var U = new Voltage(20, Unit.SI_Prefix.milli);
            var I = new Current(5, Unit.SI_Prefix.milli);
            var R = new Resistance(1, Unit.SI_Prefix.hekto);

            Power P2 = (R * QuantityBase.Pow(I, 2)).ToPrefix(Unit.SI_Prefix.mega);

            Power P3 = (QuantityBase.Pow(U, 2) / R).ToPrefix(Unit.SI_Prefix.kilo);

            var t = new Time(1, Units.Hour);
            
            Energy e = (P2 - P3) * t;

        }

        [Fact]
        public void TestImpulse1()
        {
            var q = new MyImpulseQuantity(10, MyImpulseUnit.myImpulseUnit);
            var s = new Speed(10);

            Energy e = (q * s).ToPrefix(Unit.SI_Prefix.kilo);
        }

        [Fact]
        public void TestIdealGasLaw()
        {
            // Ideal gas law
            var p = new Pressure(1013, Units.Bar, Unit.SI_Prefix.milli);

            p.SetUnit(Units.mmHg);

            var T = new Temperature(28, Units.Celsius); // 28 degrees celsius in Kelvin
            var n = new AmountOfSubstance(100);

            Volume V = (Constants.GasConstant * n * T) / p;
        }


        [Fact]
        public void TestVolueFlow()
        {
            var f = new VolumeFlow(10);

            var v = new Volume(100);

            var he = new HourEquivalent();

            var h = new Hour();

            var t = new Time(60);

            VolumeFlow f2 = v / t;

            var str = f2.ToString();
        }

        [Fact]
        public void TestVolumes()
        {
            var v1 = new Volume(10, Units.QubicMetre, Unit.SI_Prefix.mega);

            var v2 = new Volume(10, Units.HourEquivalent);

            var v3 = v1 + v2;

            var test = v1.ToString();

            var v4 = v1.ValueInSIUnits;

            v1.SetUnit(Units.HourEquivalent);

            var q = new Quantity(1, Units.HourEquivalent);

            Volume q2 = q.ToUnit(Units.QubicHectoMetre);

            Volume q3 = q2.ConvertToUnit(Units.HourEquivalent);
        }


        [Fact]
        public void TestMAssFlow()
        {
            var f1 = new MassFlow(100);
            f1.SetUnit(new TonnesPerHour());

            var f2 = new MassFlow(100);

            var f3 = f1 + f2;
        }

        [Fact]
        public void TestToDerivedUnit1()
        {
            var u1 = (Units.Joule / Units.Second).ToDerivedUnit();

            var p1 = new Power(100, u1);

            Assert.True(p1.Unit!.Equals(Units.Watt));
        }

        [Fact]
        public void TestToDerivedUnit2()
        {
            var u = (Units.WattHour / Units.Hour).ToDerivedUnit();

            var p2 = new Power(100, u, Unit.SI_Prefix.mega);

            Assert.True(p2.Unit!.Equals(Units.Watt));
        }

        [Fact]
        public void TestToDerivedUnit3()
        {
            var u = Units.Watt*Units.Hour;

            Assert.True(u.Equals(Units.WattHour));
        }

        [Fact]
        public void TestWaterValue1()
        {
            var p = new MonetaryAmount(100);

            var v = new Volume(40);

            var WaterValue = p / v;

            string name = WaterValue.Unit!.ToString();
        }

        [Fact]
        public void TestCurrencyExchange()
        {
            Currencies.NorwegianCrown!.ExchangeRateToEur = 0.122098533;
            Currencies.SwedishCrown!.ExchangeRateToEur = 0.108485427;

            var amount1 = new MonetaryAmount(100, Currencies.NorwegianCrown);
            var amount2 = new MonetaryAmount(100, Currencies.SwedishCrown);

            MonetaryAmount amount3 = (amount1 + amount2).ConvertToUnit(Currencies.USDollar);
            var diff = amount1 - amount2;

            MonetaryAmount amount4 = amount1.ConvertToUnit(Currencies.Euro);

        }

        [Fact]
        public void TestUnitPrice1()
        {
            var p1 = new UnitPrice(100, new PriceUnit(Currencies.Euro, new BTU(Unit.SI_Prefix.mega)));

            var p2 = new UnitPrice(100, new PriceUnit(Currencies.USDollar, new WattHour(Unit.SI_Prefix.mega)));


            if (p1.Price > p2.Price)
            {
                Console.Out.Write("P1 is cheaper");
            }
            var s = p1.ToString();

            var p3 = p1.ConvertToUnit(p2.PriceUnit);
        }

        [Fact]
        public void TestToDerivedunit2()
        {
            var u1 = Units.Watt * Units.Hour;

            var test = u1 == Units.WattHour;

            var u = u1.ToDerivedUnit();

            var e1 = new Energy(100, u, Unit.SI_Prefix.kilo);
            var e2 = new Energy(100, Units.WattHour);

            var e3 = e1 + e2;
        }

        [Fact]
        public void TestGravitation1()
        {
            var m = new Mass(90);

            Force f = Constants.GravityOfEarth * m;

            var h = new Length(10);

            Energy e = (f * h).ToUnit(Units.WattHour);

            e.SetPrefix(Unit.SI_Prefix.kilo);

            Energy e2 = e.ConvertToUnit(Units.WattHour);
        }

        [Fact]
        public void TestNewtonsSecondLaw()
        {
            var m = new Mass(90, new Kilogram(Unit.SI_Prefix.mega));
            var a = new Acceleration(10);

            Force f =  m * a;

            f.AdjustPrefix();
            
        }

        [Fact]
        public void TestHeatingValue()
        {
            var h1 = new HeatingValue(46858);
            var d = new Density(0.0008);

            var u = new WattHour(Unit.SI_Prefix.mega) / new QubicMetre();

            var q = h1 * d;

            var q2 = q.ConvertToUnit(u).AdjustPrefix();

            var q3 = q.ToUnit(u);
        }

        [Fact]
        public void TestEnergy()
        {
            var e1 = new Energy(1, Unit.SI_Prefix.mega);
            var e2 = new Energy(1002, Unit.SI_Prefix.kilo);

            Assert.True(e1 <= e2);
        }

        [Fact]
        public void TestPowerToEnergy()
        {
            Power p1 = new (1, Unit.SI_Prefix.mega);
            Time t = new Time(3600);

            Unit? MWh = new MegaWattHour();

            Energy e = p1 * t;

            e.SetUnit(MWh);

            var e1 = e.ConvertToUnit(MWh);
            
            Assert.True(Math.Abs(e1.Value - 1.0) < 1e-6);

            Assert.True(e1.Unit!.Equals(MWh));
        }

        [Fact]
        public void TestToDerived()
        {
            Force f = new Force(100, Unit.SI_Prefix.kilo);
            Length l = new Length(100);
            Time t = new Time(3600);

            var P = (f * l / t).ConvertToDerivedUnit();

            

            Assert.True(P.Unit!.Equals(Units.Watt));

        }

        [Fact]
        public void TestEnergy2()
        {
            Unit? GWh = new WattHour(Unit.SI_Prefix.giga);
            Unit? MWh = new WattHour(Unit.SI_Prefix.mega);
            Energy e1 = new(300, GWh);
            var e2 = e1.ConvertToUnit(MWh);

            Assert.True(e2.Value.Equals(3e5));

        }


        [Fact]
        public void PercentageTest()
        {
            var e1 = new Energy(1, Unit.SI_Prefix.mega);
            var p1 = new Percentage(10);
            var p2 = new Percentage(25);

            Energy e3 = ((p1 + p2) * e1).ConvertToUnit(Units.MegaWattHour);

            Assert.Equal(3.5e5, e3.ValueInSIUnits,3);
        }

        [Fact]
        public void MegaWattHourTest()
        {
            var e1 = new Energy(1, Unit.SI_Prefix.mega);
            Energy e2 = e1.ConvertToUnit(Units.WattHour);

            Energy e3 = e1.ConvertToUnit(new WattHour(Unit.SI_Prefix.mega));
        }

        [Fact]
        public void KiloWattHourTest()
        {
            // 1 kWh = 3 600 000 J
            var e1 = new Energy(1, Units.KiloWattHour!);
            Assert.Equal(3.6e6, e1.ValueInSIUnits, 3);

            // 1 MWh = 1000 kWh
            var e2 = new Energy(1, Units.MegaWattHour!);
            Energy e3 = e2.ConvertToUnit(Units.KiloWattHour!);
            Assert.Equal(1000.0, e3.Value, 3);

            // Registry lookup
            Assert.NotNull(UnitRegistry.TryGet("KiloWattHour"));
            Assert.Equal("KiloWattHour", UnitRegistry.TryGetName(Units.KiloWattHour!));
        }

        [Fact]
        public void HeatingValueTest()
        {
            var lhv1 = new HeatingValue(1);

            var u = (new WattHour(Unit.SI_Prefix.mega)) / (new Kilogram());

            HeatingValue lhv2 = lhv1.ConvertToUnit(u);
        }

        [Fact]
        public void SpecificEnergyTest()
        {
            var u = (new WattHour(Unit.SI_Prefix.kilo)) / (new Kilogram(Unit.SI_Prefix.kilo)); // kW/ton

            var e = new SpecificEnergy(7560.0, u);

            var u1 = (new Joule(Unit.SI_Prefix.giga)) / (new Kilogram(Unit.SI_Prefix.kilo)); // kW/ton

            SpecificEnergy e2 = e.ConvertToUnit(u1);
        }

        [Fact]
        public void EnergyEquivalentTest()
        {
            var u = (new WattHour(Unit.SI_Prefix.kilo)) / (new QubicMetre()); // kWh/m^3

            var e = new EnergyEquivalent(1.0, u);

            var u1 = (new WattHour(Unit.SI_Prefix.mega)) / (new QubicHectoMetre()); // MWh/MM3

            EnergyEquivalent e2 = e.ConvertToUnit(u1);
        }

        [Fact]
        public void EnergyEquivalentSumTest()
        {
            var u = (new WattHour(Unit.SI_Prefix.mega)) / (new QubicHectoMetre()); // MWh/MM3

            var e1 = new EnergyEquivalent(0.0, u);
            var e2 = new EnergyEquivalent(0.41, u);


            EnergyEquivalent sum = e1 + e2;

            Assert.True(sum.Value.Equals(0.41));
        }

        [Fact]
        public void AreaLengthVolumeTest()
        {
            var v = new Volume(1, new QubicHectoMetre());

            var l = new Length(10);

            Area a = v / l;

            a = a.ConvertToUnit(new SquareKilometer());


            Assert.True(Math.Abs(a.Value - 0.1) < 1e-3);
        }

        [Fact]
        public void ScaleTest()
        {
            var v = new Volume(1, new QubicHectoMetre());

            var v2 = new Volume(10, Units.Litre);

            var fac = v / v2;
        }

        [Fact]
        public void TestIncompatibleUnits()
        {

            Length l = new Length(10);

            Time t = new Time(5, Units.Hour);

            Assert.Throws<IncompatibleUnits>(() =>
            {
                var test = l + t;
            });


        }

        [Fact]
        public void TestIncompatibleUnits2()
        {

            Length l = new Length(10);

            Force f = new Force(100, Units.Newton, Unit.SI_Prefix.kilo);

            Assert.Throws<IncompatibleUnits>(() =>
            {
                Power p = f * l;
            });


        }

        [Fact]
        public void ReflectionTest()
        {
            Length l = new Length(10, new Metre(), Unit.SI_Prefix.milli);
            Unit? matched = null;
            foreach (Unit? unit in Units.UnitList)
            {
                if (l.Unit == unit)
                {
                    matched = unit; 
                    break;
                }
            }

            Assert.True(matched == Units.Metre);
        }

        [Fact]
        public void ReflectionTest2()
        {
            Mass m = new Mass(2);
            Speed s = new Speed(3);
            var kinetic = 0.5 * m * s * s;

            Unit? matched = null;
            foreach (Unit? unit in Units.UnitList)
            {
                if (kinetic.Unit == unit)
                {
                    matched = unit;
                    break;
                }
            }

            Assert.True(matched == Units.Joule);
        }

        [Fact]
        public void TestHorsePower()
        {
            var EnginePower1 = new Power(450, Units.HorsePower);
            var EnginePower2 = new Power(450, new Watt(Unit.SI_Prefix.kilo));

            bool test = EnginePower2 > EnginePower1;

            Assert.True(test);

        }

        public class MyImpulseUnit : Unit
        {
            public static readonly MyImpulseUnit? myImpulseUnit = new MyImpulseUnit();

            private MyImpulseUnit() : base(1, 1, -1, 0, 0, 0, 0)
            {
                Scale = 3.1415;
            }
        }

        private class MyImpulseQuantity : QuantityBase
        {
            public MyImpulseQuantity(double val, Unit? u) : base(val, u)
            {
            }
        }

        [Fact]
        public void TestDetectingUnits()
        {
            Length lc = new Length(10, Unit.SI_Prefix.centi);
            Length lm = new Length(10, Unit.SI_Prefix.milli);
            Length lk = new Length(10, Unit.SI_Prefix.hekto);

            Length L = lk + 2 * lm + 5 * lc;

            var test1 = L.Unit.ToDerivedUnit();
            var test2 = L.Unit.ToBaseUnit();
            string label = L.ToString();

        }




        // ── Electrical quantities ──────────────────────────────────────────────

        [Fact]
        public void TestCapacitanceEnergyStored()
        {
            // E = ½ C V²
            var C = new Capacitance(10, Unit.SI_Prefix.mikro); // 10 µF
            var V = new Voltage(100);                              // 100 V
            Energy E = 0.5 * C * V * V;
            Assert.True(Math.Abs(E.ValueInSIUnits - 0.05) < 1e-9);
        }

        [Fact]
        public void TestCapacitanceAddition()
        {
            var c1 = new Capacitance(100, Unit.SI_Prefix.mikro);
            var c2 = new Capacitance(200, Unit.SI_Prefix.mikro);
            Capacitance c3 = c1 + c2;
            Assert.True(Math.Abs(c3.ValueInSIUnits - 300e-6) < 1e-12);
        }

        [Fact]
        public void TestInductanceEnergyStored()
        {
            // E = ½ L I²
            var L = new Inductance(2, Unit.SI_Prefix.milli); // 2 mH
            var I = new Current(5);                              // 5 A
            Energy E = 0.5 * L * I * I;
            Assert.True(Math.Abs(E.ValueInSIUnits - 0.025) < 1e-9);
        }

        [Fact]
        public void TestInductanceAddition()
        {
            var l1 = new Inductance(1);
            var l2 = new Inductance(2);
            Inductance l3 = l1 + l2;
            Assert.True(Math.Abs(l3.ValueInSIUnits - 3.0) < 1e-9);
        }

        [Fact]
        public void TestMagneticFluxFromDensityAndArea()
        {
            // Φ = B × A  (Weber = Tesla × m²)
            var B = new MagneticFluxDensity(0.5); // 0.5 T
            var A = new Area(2.0);                // 2 m²
            var phi = B * A;
            Assert.True(phi.Unit!.SameDimension(Units.Weber));
            Assert.True(Math.Abs(phi.ValueInSIUnits - 1.0) < 1e-9);
        }

        [Fact]
        public void TestMagneticFluxAddition()
        {
            var f1 = new MagneticFlux(0.3);
            var f2 = new MagneticFlux(0.7);
            MagneticFlux f3 = f1 + f2;
            Assert.True(Math.Abs(f3.ValueInSIUnits - 1.0) < 1e-9);
        }

        [Fact]
        public void TestMagneticFluxIntensityUnit()
        {
            // H field: 1000 A/m is a typical value inside a solenoid
            var H = new MagneticFluxIntensity(1000);
            Assert.True(H.Unit!.SameDimension(Units.AmperePerMetre));
            Assert.True(Math.Abs(H.ValueInSIUnits - 1000.0) < 1e-9);
        }

        [Fact]
        public void TestMagneticFluxIntensityAddition()
        {
            var h1 = new MagneticFluxIntensity(500);
            var h2 = new MagneticFluxIntensity(300);
            MagneticFluxIntensity h3 = h1 + h2;
            Assert.True(Math.Abs(h3.ValueInSIUnits - 800.0) < 1e-9);
        }

        [Fact]
        public void TestMagneticFluxDensitySubtraction()
        {
            var b1 = new MagneticFluxDensity(1.5);
            var b2 = new MagneticFluxDensity(0.5);
            MagneticFluxDensity b3 = b1 - b2;
            Assert.True(Math.Abs(b3.ValueInSIUnits - 1.0) < 1e-9);
        }

        [Fact]
        public void TestVoltageSubtraction()
        {
            var v1 = new Voltage(230);
            var v2 = new Voltage(12);
            Voltage v3 = v1 - v2;
            Assert.True(Math.Abs(v3.ValueInSIUnits - 218.0) < 1e-9);
        }

        [Fact]
        public void TestCurrentSubtraction()
        {
            var i1 = new Current(10);
            var i2 = new Current(3);
            Current i3 = i1 - i2;
            Assert.True(Math.Abs(i3.ValueInSIUnits - 7.0) < 1e-9);
        }

        [Fact]
        public void TestResistanceAddition()
        {
            // Series resistors: R_total = R1 + R2
            var r1 = new Resistance(100);
            var r2 = new Resistance(220);
            Resistance r3 = r1 + r2;
            Assert.True(Math.Abs(r3.ValueInSIUnits - 320.0) < 1e-9);
        }

        // ── Chemical / luminous quantities ─────────────────────────────────────

        [Fact]
        public void TestAmountOfSubstanceAddition()
        {
            var n1 = new AmountOfSubstance(1.5);
            var n2 = new AmountOfSubstance(2.5);
            AmountOfSubstance n3 = n1 + n2;
            Assert.True(Math.Abs(n3.ValueInSIUnits - 4.0) < 1e-9);
        }

        [Fact]
        public void TestAmountOfSubstanceSubtraction()
        {
            var n1 = new AmountOfSubstance(5.0);
            var n2 = new AmountOfSubstance(3.0);
            AmountOfSubstance n3 = n1 - n2;
            Assert.True(Math.Abs(n3.ValueInSIUnits - 2.0) < 1e-9);
        }

        [Fact]
        public void TestLuminousIntensityAddition()
        {
            var c1 = new LuminousIntensity(100);
            var c2 = new LuminousIntensity(50);
            LuminousIntensity c3 = c1 + c2;
            Assert.True(Math.Abs(c3.ValueInSIUnits - 150.0) < 1e-9);
        }

        [Fact]
        public void TestCatalyticActivityAddition()
        {
            var k1 = new CatalyticActivity(2.0);
            var k2 = new CatalyticActivity(3.0);
            CatalyticActivity k3 = k1 + k2;
            Assert.True(Math.Abs(k3.ValueInSIUnits - 5.0) < 1e-9);
        }

        // ── Comparison operators ───────────────────────────────────────────────

        [Fact]
        public void TestComparisonOperators()
        {
            var p1 = new Power(100);
            var p2 = new Power(200);
            Assert.True(p1 < p2);
            Assert.True(p2 > p1);
            Assert.True(p1 <= p2);
            Assert.True(p2 >= p1);
            Assert.False(p1 > p2);
            Assert.False(p2 < p1);
        }

        [Fact]
        public void TestComparisonEqualValues()
        {
            var e1 = new Energy(1, Unit.SI_Prefix.kilo);
            var e2 = new Energy(1000);
            Assert.True(e1 <= e2);
            Assert.True(e1 >= e2);
        }

        // ── IComparable<T> / LINQ ─────────────────────────────────────────────

        [Fact]
        public void TestLinqMinMax()
        {
            var powers = new List<Power>
            {
                new Power(300, Unit.SI_Prefix.mega),
                new Power(100, Unit.SI_Prefix.mega),
                new Power(500, Unit.SI_Prefix.mega),
                new Power(200, Unit.SI_Prefix.mega),
            };

            Assert.Equal(100e6, powers.Min()!.ValueInSIUnits, precision: 3);
            Assert.Equal(500e6, powers.Max()!.ValueInSIUnits, precision: 3);
        }

        [Fact]
        public void TestLinqSort()
        {
            var energies = new List<Energy>
            {
                new Energy(3, Units.MegaWattHour),
                new Energy(1, Units.MegaWattHour),
                new Energy(2, Units.MegaWattHour),
            };

            var sorted = energies.OrderBy(e => e).ToList();

            Assert.True(sorted[0].ValueInSIUnits < sorted[1].ValueInSIUnits);
            Assert.True(sorted[1].ValueInSIUnits < sorted[2].ValueInSIUnits);
        }

        [Fact]
        public void TestCompareTo_LessThan()
        {
            var t1 = new Temperature(20);
            var t2 = new Temperature(100);
            Assert.True(t1.CompareTo(t2) < 0);
            Assert.True(t2.CompareTo(t1) > 0);
            Assert.Equal(0, t1.CompareTo(new Temperature(20)));
        }

        [Fact]
        public void TestCompareTo_Null()
        {
            var p = new Power(100);
            Assert.True(p.CompareTo(null) > 0);
        }

        // ── Equality operators ────────────────────────────────────────────────

        [Fact]
        public void TestEqualityOperator_SameValue()
        {
            var e1 = new Energy(1000);
            var e2 = new Energy(1000);
            Assert.True(e1 == e2);
            Assert.False(e1 != e2);
        }

        [Fact]
        public void TestEqualityOperator_EquivalentPrefix()
        {
            // 1 kJ == 1000 J — same SI value, different representation
            var e1 = new Energy(1, Unit.SI_Prefix.kilo);
            var e2 = new Energy(1000);
            Assert.True(e1 == e2);
        }

        [Fact]
        public void TestEqualityOperator_DifferentValue()
        {
            var p1 = new Power(100);
            var p2 = new Power(200);
            Assert.False(p1 == p2);
            Assert.True(p1 != p2);
        }

        [Fact]
        public void TestEqualityOperator_Null()
        {
            var e = new Energy(1);
            Assert.False(e == null);
            Assert.False(null == e);
            Assert.True((QuantityBase?)null == (QuantityBase?)null);
        }

        // ── Clone ──────────────────────────────────────────────────────────────

        [Fact]
        public void TestCloneIsEqualButDistinct()
        {
            var p = new Power(500, Unit.SI_Prefix.kilo);
            var clone = p.Clone();
            Assert.True(p.Equals(clone));
            Assert.False(ReferenceEquals(p, clone));
        }

        // ── ConvertToUnit ─────────────────────────────────────────────────────

        [Fact]
        public void TestConvertPressureUnit()
        {
            // 1 bar = 100 000 Pa  (kg·m⁻¹·s⁻²)
            var bar = new Unit(-1, 1, -2, 0, 0, 0, 0, 1e5);
            var p = new Pressure(1.5, bar);
            Pressure pa = p.ConvertToUnit(Units.Pascal);
            Assert.True(Math.Abs(pa.ValueInSIUnits - 1.5e5) < 1.0);
        }

        [Fact]
        public void TestConvertMassFlowUnit()
        {
            // 1 tonne/h = 1000 kg / 3600 s ≈ 0.2778 kg/s
            var tonnesPerHour = new Kilogram(Unit.SI_Prefix.kilo) / new Hour();
            var mf = new MassFlow(1.0, tonnesPerHour);
            MassFlow kgPerSec = mf.ConvertToUnit(Units.KilogramPerSecond);
            Assert.True(Math.Abs(kgPerSec.ValueInSIUnits - 1000.0 / 3600.0) < 1e-9);
        }

        // ── AdjustPrefix ──────────────────────────────────────────────────────

        [Fact]
        public void TestAdjustPrefix()
        {
            var p = new Power(1_500_000); // 1.5 MW stored as W
            p.AdjustPrefix();
            // Should select mega prefix so display value is close to 1
            Assert.True(Math.Abs(p.Value - 1.5) < 0.01);
            Assert.Equal(Unit.SI_Prefix.mega, p.PrefixIndex);
        }

        // ── Frequency ─────────────────────────────────────────────────────────

        [Fact]
        public void TestFrequencyAddition()
        {
            var f1 = new Frequency(440); // A4 note
            var f2 = new Frequency(110);
            Frequency f3 = f1 + f2;
            Assert.True(Math.Abs(f3.ValueInSIUnits - 550.0) < 1e-9);
        }

        [Fact]
        public void TestFrequencyPeriod()
        {
            // T = 1/f  → period of 50 Hz grid is 0.02 s
            var f = new Frequency(50);
            var T = 1.0 / f;
            Assert.True(Math.Abs(T.ValueInSIUnits - 0.02) < 1e-9);
        }

        [Fact]
        public void TestFrequencyKilohertz()
        {
            var f = new Frequency(1, Unit.SI_Prefix.kilo); // 1 kHz
            Assert.True(Math.Abs(f.ValueInSIUnits - 1000.0) < 1e-9);
        }

        // ── ElectricCharge ────────────────────────────────────────────────────

        [Fact]
        public void TestElectricChargeFromCurrentAndTime()
        {
            // Q = I × t  → 2 A for 30 s = 60 C
            var I = new Current(2);
            var t = new Time(30);
            var Q = I * t;
            Assert.True(Q.Unit!.SameDimension(Units.Coulomb));
            Assert.True(Math.Abs(Q.ValueInSIUnits - 60.0) < 1e-9);
        }

        [Fact]
        public void TestElectricChargeAddition()
        {
            var q1 = new ElectricCharge(1000);
            var q2 = new ElectricCharge(2000);
            ElectricCharge q3 = q1 + q2;
            Assert.True(Math.Abs(q3.ValueInSIUnits - 3000.0) < 1e-9);
        }

        [Fact]
        public void TestAmpereHourConversion()
        {
            // 1 Ah = 3600 C
            var q = new ElectricCharge(1, new AmpereHour());
            ElectricCharge inCoulombs = q.ConvertToUnit(Units.Coulomb);
            Assert.True(Math.Abs(inCoulombs.ValueInSIUnits - 3600.0) < 1e-9);
        }

        // ── IncompatibleUnits for electrical types ─────────────────────────────

        [Fact]
        public void TestIncompatibleUnitsVoltageAndCurrent()
        {
            var v = new Voltage(230);
            var i = new Current(10);
            Assert.Throws<IncompatibleUnits>(() =>
            {
                Voltage bad = v + i;
            });
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        // -------------------------------------------------------------------------
        // UnitList auto-population
        // -------------------------------------------------------------------------

        [Fact]
        public void UnitList_ContainsAllRegisteredUnits()
        {
            // UnitList should contain all public static Unit fields from the Units class
            Assert.True(Units.UnitList.Count > 6, "UnitList should have more than the old hard-coded 6 entries");
            Assert.Contains(Units.Joule,      Units.UnitList);
            Assert.Contains(Units.MegaWatt,   Units.UnitList);
            Assert.Contains(Units.KiloWattHour, Units.UnitList);
            Assert.Contains(Units.Hertz,      Units.UnitList);
            Assert.Contains(Units.Coulomb,    Units.UnitList);
            Assert.Contains(Units.AmperePerMetre, Units.UnitList);
        }

        // -------------------------------------------------------------------------
        // Constructor consistency
        // -------------------------------------------------------------------------

        [Fact]
        public void ConstructorConsistency_MassPrefix()
        {
            var m1 = new Mass(1.5, Unit.SI_Prefix.kilo); // 1.5 t (kilogram * kilo = 1500 kg... actually kilo*kilogram = 1500 kg)
            var m2 = new Mass(1500);
            Assert.Equal(m2.ValueInSIUnits, m1.ValueInSIUnits, 6);
        }

        [Fact]
        public void ConstructorConsistency_SpeedPrefix()
        {
            var s1 = new Speed(1.0, Unit.SI_Prefix.kilo); // 1 km/s = 1000 m/s
            var s2 = new Speed(1000.0);
            Assert.Equal(s2.ValueInSIUnits, s1.ValueInSIUnits, 6);
        }

        [Fact]
        public void ConstructorConsistency_AccelerationPrefix()
        {
            var a1 = new Acceleration(1.0, Unit.SI_Prefix.milli); // 0.001 m/s²
            var a2 = new Acceleration(0.001);
            Assert.Equal(a2.ValueInSIUnits, a1.ValueInSIUnits, 9);
        }

        [Fact]
        public void ConstructorConsistency_TemperaturePrefix()
        {
            var t1 = new Temperature(1.0, Unit.SI_Prefix.kilo); // 1000 K
            var t2 = new Temperature(1000.0);
            Assert.Equal(t2.ValueInSIUnits, t1.ValueInSIUnits, 6);
        }

        [Fact]
        public void ConstructorConsistency_ElectricChargeNoPrefix()
        {
            var q1 = new ElectricCharge(3600.0);           // 3600 C bare ctor
            var q2 = new ElectricCharge(3600.0, Units.Coulomb!);
            Assert.Equal(q2.ValueInSIUnits, q1.ValueInSIUnits, 6);
        }
    }
}