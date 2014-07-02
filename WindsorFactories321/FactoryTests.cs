using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using System.Diagnostics;
using System.Globalization;

namespace WindsorFactories
{
    [TestClass]
    public sealed class FactoryTests
    {
        private IWindsorContainer container;

        [TestInitialize]
        public void TestInitialize()
        {
            Controls.DisposeFactories = true;
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "TestInitialize, creating container"));
            this.container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();
            container.Register(
                Component.For<IFoo>().ImplementedBy<FooA>().Named("FooA").LifestyleTransient(),
                Component.For<IFoo>().ImplementedBy<FooB>().Named("FooB").LifestyleTransient(),
                Component.For<IFoo>().ImplementedBy<FooC>().Named("FooC").LifestyleTransient(),
                Component.For<IFoo>().ImplementedBy<FooD>().Named("FooD").LifestyleTransient(),
                Component.For<IFooFactory>().AsFactory().LifestyleTransient(),
                Component.For<IFooConsumer>().ImplementedBy<FooConsumer>().LifestyleTransient(),
                Component.For<IFooConsumerFactory>().AsFactory().LifestyleTransient(),
                Component.For<IFooService>().ImplementedBy<FooService>().LifestyleSingleton(),
                Component.For<IGizmo>().ImplementedBy<Gizmo>().LifestyleTransient(),
                Component.For<IGizmoFactory>().AsFactory().LifestyleTransient(),
                Component.For<IGizmoService>().ImplementedBy<GizmoService>().LifestyleSingleton()
                );
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "TestCleanup, disposing container"));
            this.container.Dispose();
        }

        [TestMethod]
        public void Complex()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving fooConsumerFactory"));
            var fcf = this.container.Resolve<IFooConsumerFactory>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained fooConsumerFactory #" + fcf.GetHashCode()));
            
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, creating fooConsumer using factory #" + fcf.GetHashCode()));
            var fc = fcf.Create();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, created fooConsumer #" + fc.GetHashCode() + " from factory #" + fcf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbing fooConsumer #" + fc.GetHashCode()));
            fc.Frob();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbed fooConsumer #" + fc.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing fooConsumer #" + fc.GetHashCode() + " to factory #" + fcf.GetHashCode()));
            fcf.Release(fc);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released fooConsumer #" + fc.GetHashCode() + " to factory #" + fcf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving fooService"));
            var fs = this.container.Resolve<IFooService>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained fooService #" + fs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, snarfing fooService #" + fs.GetHashCode()));
            fs.Snarf();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, snarfing fooService #" + fs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, creating fooConsumer using factory #" + fcf.GetHashCode()));
            fc = fcf.Create();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, created fooConsumer #" + fc.GetHashCode() + " from factory #" + fcf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbing fooConsumer #" + fc.GetHashCode()));
            fc.Frob();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbed fooConsumer #" + fc.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing fooConsumer #" + fc.GetHashCode() + " to factory #" + fcf.GetHashCode()));
            fcf.Release(fc);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released fooConsumer #" + fc.GetHashCode() + " to factory #" + fcf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing fooService #" + fs.GetHashCode()));
            this.container.Release(fs);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released fooService #" + fs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing fooConsumerFactory #" + fcf.GetHashCode()));
            this.container.Release(fcf);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released fooConsumerFactory #" + fcf.GetHashCode()));
        }

        [TestMethod]
        public void Complex_no_factory_dispose()
        {
            Controls.DisposeFactories = false;
            Complex();
        }

        [TestMethod]
        public void Simple()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving fooFactory"));
            var ff = this.container.Resolve<IFooFactory>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained fooFactory #" + ff.GetHashCode()));
            
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, creating fooC using factory #" + ff.GetHashCode()));
            var f = ff.GetFooC();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained fooC #" + f.GetHashCode() + " from factory #" + ff.GetHashCode()));
            
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulating fooC #" + f.GetHashCode()));
            f.Frobulate();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulated fooC #" + f.GetHashCode()));
            
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, returning fooC #" + f.GetHashCode() + " to factory #" + ff.GetHashCode()));
            ff.Release(f);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, returned fooC #" + f.GetHashCode() + " to factory #" + ff.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing fooFactory #" + ff.GetHashCode()));
            this.container.Release(ff);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released fooFactory #" + ff.GetHashCode()));
            
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving gizmoService"));
            var gs = this.container.Resolve<IGizmoService>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmoService #" + gs.GetHashCode()));
            
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbing gizmoService #" + gs.GetHashCode()));
            gs.Frob();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbed gizmoService #" + gs.GetHashCode()));
            
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing gizmoService #" + gs.GetHashCode()));
            this.container.Release(gs);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released gizmoService #" + gs.GetHashCode()));
        }

        [TestMethod]
        public void Simple_no_factory_dispose()
        {
            Controls.DisposeFactories = false;
            Simple();
        }

        [TestMethod]
        public void Simple2()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving fooFactory"));
            var ff = this.container.Resolve<IFooFactory>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained fooFactory #" + ff.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, creating fooC using factory #" + ff.GetHashCode()));
            var f = ff.GetFooC();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained fooC #" + f.GetHashCode() + " from factory #" + ff.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulating fooC #" + f.GetHashCode()));
            f.Frobulate();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulated fooC #" + f.GetHashCode()));

            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, returning fooC #" + f.GetHashCode() + " to factory #" + ff.GetHashCode()));
            //ff.Release(f);
            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, returned fooC #" + f.GetHashCode() + " to factory #" + ff.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing fooFactory #" + ff.GetHashCode()));
            this.container.Release(ff);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released fooFactory #" + ff.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving gizmoService"));
            var gs = this.container.Resolve<IGizmoService>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmoService #" + gs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbing gizmoService #" + gs.GetHashCode()));
            gs.Frob();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbed gizmoService #" + gs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing gizmoService #" + gs.GetHashCode()));
            this.container.Release(gs);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released gizmoService #" + gs.GetHashCode()));
        }

        [TestMethod]
        public void Simple2_no_factory_dispose()
        {
            Controls.DisposeFactories = false;
            Simple2();
        }

        [TestMethod]
        public void Simple3()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving fooFactory"));
            var ff = this.container.Resolve<IFooFactory>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained fooFactory #" + ff.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, creating fooC using factory #" + ff.GetHashCode()));
            var f = ff.GetFooC();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained fooC #" + f.GetHashCode() + " from factory #" + ff.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulating fooC #" + f.GetHashCode()));
            f.Frobulate();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulated fooC #" + f.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, returning fooC #" + f.GetHashCode() + " to factory #" + ff.GetHashCode()));
            ff.Release(f);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, returned fooC #" + f.GetHashCode() + " to factory #" + ff.GetHashCode()));

            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing fooFactory #" + ff.GetHashCode()));
            //this.container.Release(ff);
            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released fooFactory #" + ff.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving gizmoService"));
            var gs = this.container.Resolve<IGizmoService>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmoService #" + gs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbing gizmoService #" + gs.GetHashCode()));
            gs.Frob();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbed gizmoService #" + gs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing gizmoService #" + gs.GetHashCode()));
            this.container.Release(gs);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released gizmoService #" + gs.GetHashCode()));
        }

        [TestMethod]
        public void Simple3_no_factory_dispose()
        {
            Controls.DisposeFactories = false;
            Simple3();
        }

        [TestMethod]
        public void Test3()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving FooConsumer"));
            var fc = this.container.Resolve<IFooConsumer>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained FooConsumer #" + fc.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving gizmoService"));
            var gs = this.container.Resolve<IGizmoService>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmoService #" + gs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbing FooConsumer #" + fc.GetHashCode()));
            fc.Frob();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbed FooConsumer #" + fc.GetHashCode()));
            
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbing gizmoService #" + gs.GetHashCode()));
            gs.Frob();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbed gizmoService #" + gs.GetHashCode()));
            
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing gizmoService #" + gs.GetHashCode()));
            this.container.Release(gs);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released gizmoService #" + gs.GetHashCode()));
            
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing FooConsumer #" + fc.GetHashCode()));
            this.container.Release(fc);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released FooConsumer #" + fc.GetHashCode()));
        }

        [TestMethod]
        public void Test3_no_factory_dispose()
        {
            Controls.DisposeFactories = false;
            Test3();
        }

        [TestMethod]
        public void Test4()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving fooConsumerFactory"));
            var fcf = this.container.Resolve<IFooConsumerFactory>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained fooConsumerFactory #" + fcf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, creating fooConsumer using factory #" + fcf.GetHashCode()));
            var fc = fcf.Create();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, created fooConsumer #" + fc.GetHashCode() + " from factory #" + fcf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbing fooConsumer #" + fc.GetHashCode()));
            fc.Frob();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbed fooConsumer #" + fc.GetHashCode()));

            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing fooConsumer #" + fc.GetHashCode() + " to factory #" + fcf.GetHashCode()));
            //fcf.Release(fc);
            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released fooConsumer #" + fc.GetHashCode() + " to factory #" + fcf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing fooConsumerFactory #" + fcf.GetHashCode()));
            this.container.Release(fcf);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released fooConsumerFactory #" + fcf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving gizmoService"));
            var gs = this.container.Resolve<IGizmoService>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmoService #" + gs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbing gizmoService #" + gs.GetHashCode()));
            gs.Frob();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbed gizmoService #" + gs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing gizmoService #" + gs.GetHashCode()));
            this.container.Release(gs);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released gizmoService #" + gs.GetHashCode()));
        }

        [TestMethod]
        public void Test4_no_factory_dispose()
        {
            Controls.DisposeFactories = false;
            Test4();
        }

        [TestMethod]
        public void Simpler()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving FooC"));
            var f = this.container.Resolve<IFoo>("FooC");
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained FooC #" + f.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulating FooC #" + f.GetHashCode()));
            f.Frobulate();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulated FooC #" + f.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing FooC #" + f.GetHashCode()));
            this.container.Release(f);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released FooC #" + f.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving gizmoService"));
            var gs = this.container.Resolve<IGizmoService>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmoService #" + gs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbing gizmoService #" + gs.GetHashCode()));
            gs.Frob();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbed gizmoService #" + gs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing gizmoService #" + gs.GetHashCode()));
            this.container.Release(gs);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released gizmoService #" + gs.GetHashCode()));
        }

        [TestMethod]
        public void Simpler_no_factory_dispose()
        {
            Controls.DisposeFactories = false;
            Simpler();
        }

        [TestMethod]
        public void Simpler2()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving FooC"));
            var f = this.container.Resolve<IFoo>("FooC");
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained FooC #" + f.GetHashCode()));

            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulating FooC #" + f.GetHashCode()));
            //f.Frobulate();
            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulated FooC #" + f.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing FooC #" + f.GetHashCode()));
            this.container.Release(f);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released FooC #" + f.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving gizmoService"));
            var gs = this.container.Resolve<IGizmoService>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmoService #" + gs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbing gizmoService #" + gs.GetHashCode()));
            gs.Frob();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbed gizmoService #" + gs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing gizmoService #" + gs.GetHashCode()));
            this.container.Release(gs);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released gizmoService #" + gs.GetHashCode()));
        }

        [TestMethod]
        public void Simpler2_no_factory_dispose()
        {
            Controls.DisposeFactories = false;
            Simpler2();
        }

        [TestMethod]
        public void Simpler3()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving FooC"));
            var f = this.container.Resolve<IFoo>("FooC");
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained FooC #" + f.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulating FooC #" + f.GetHashCode()));
            f.Frobulate();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulated FooC #" + f.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing FooC #" + f.GetHashCode()));
            this.container.Release(f);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released FooC #" + f.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving gizmoService"));
            var gs = this.container.Resolve<IGizmoService>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmoService #" + gs.GetHashCode()));

            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbing gizmoService #" + gs.GetHashCode()));
            //gs.Frob();
            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobbed gizmoService #" + gs.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing gizmoService #" + gs.GetHashCode()));
            this.container.Release(gs);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released gizmoService #" + gs.GetHashCode()));
        }

        [TestMethod]
        public void Simpler3_no_factory_dispose()
        {
            Controls.DisposeFactories = false;
            Simpler3();
        }

        [TestMethod]
        public void Dependencies_of_fabricated_components_get_disposed()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving fooFactory"));
            var ff = this.container.Resolve<IFooFactory>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained fooFactory #" + ff.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, creating fooD using factory #" + ff.GetHashCode()));
            var f = ff.GetFooD();
            var g = ((FooD)f).Gizmo;
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained fooD #" + f.GetHashCode() + " from factory #" + ff.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulating fooD #" + f.GetHashCode()));
            f.Frobulate();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulated fooD #" + f.GetHashCode()));

            Assert.AreEqual(0, g.DisposeCount);

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, returning fooD #" + f.GetHashCode() + " to factory #" + ff.GetHashCode()));
            ff.Release(f);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, returned fooD #" + f.GetHashCode() + " to factory #" + ff.GetHashCode()));

            Assert.AreEqual(1, g.DisposeCount);

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing fooFactory #" + ff.GetHashCode()));
            this.container.Release(ff);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released fooFactory #" + ff.GetHashCode()));

            Assert.AreEqual(1, g.DisposeCount);
        }

        [TestMethod]
        public void Dependencies_of_unreleased_fabricated_components_get_disposed_twice()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving fooFactory"));
            var ff = this.container.Resolve<IFooFactory>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained fooFactory #" + ff.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, creating fooD using factory #" + ff.GetHashCode()));
            var f = ff.GetFooD();
            var g = ((FooD)f).Gizmo;
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained fooD #" + f.GetHashCode() + " from factory #" + ff.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulating fooD #" + f.GetHashCode()));
            f.Frobulate();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, frobulated fooD #" + f.GetHashCode()));

            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, returning fooD #" + f.GetHashCode() + " to factory #" + ff.GetHashCode()));
            //ff.Release(f);
            //Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, returned fooD #" + f.GetHashCode() + " to factory #" + ff.GetHashCode()));

            Assert.AreEqual(0, g.DisposeCount);

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing fooFactory #" + ff.GetHashCode()));
            this.container.Release(ff);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released fooFactory #" + ff.GetHashCode()));

            Assert.AreEqual(2, g.DisposeCount);
        }

        [TestMethod]
        public void Factory_can_be_disposed_before_release()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving gizmoFactory"));
            var gf = this.container.Resolve<IGizmoFactory>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmoFactory #" + gf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, creating gizmo using factory #" + gf.GetHashCode()));
            var g = gf.Create();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmo #" + g.GetHashCode() + " from factory #" + gf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, disposing gizmoFactory #" + gf.GetHashCode()));
            gf.Dispose();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, disposed gizmoFactory #" + gf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing gizmoFactory #" + gf.GetHashCode()));
            this.container.Release(gf);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released gizmoFactory #" + gf.GetHashCode()));
        }

        [TestMethod]
        public void Factory_can_be_disposed_after_release()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving gizmoFactory"));
            var gf = this.container.Resolve<IGizmoFactory>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmoFactory #" + gf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, creating gizmo using factory #" + gf.GetHashCode()));
            var g = gf.Create();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmo #" + g.GetHashCode() + " from factory #" + gf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, releasing gizmoFactory #" + gf.GetHashCode()));
            this.container.Release(gf);
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, released gizmoFactory #" + gf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, disposing gizmoFactory #" + gf.GetHashCode()));
            gf.Dispose();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, disposed gizmoFactory #" + gf.GetHashCode()));
        }

        [TestMethod]
        public void Factory_is_double_dispose_safe()
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, resolving gizmoFactory"));
            var gf = this.container.Resolve<IGizmoFactory>();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmoFactory #" + gf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, creating gizmo using factory #" + gf.GetHashCode()));
            var g = gf.Create();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, obtained gizmo #" + g.GetHashCode() + " from factory #" + gf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, disposing gizmoFactory #" + gf.GetHashCode()));
            gf.Dispose();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, disposed gizmoFactory #" + gf.GetHashCode()));

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, disposing gizmoFactory #" + gf.GetHashCode()));
            gf.Dispose();
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} #{1} {2}", this.GetType().Name, this.GetHashCode(), "test, disposed gizmoFactory #" + gf.GetHashCode()));
        }
    }
}
