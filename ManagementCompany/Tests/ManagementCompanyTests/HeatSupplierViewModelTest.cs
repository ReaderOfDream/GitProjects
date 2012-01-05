using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ManagementCompany.Models;
using NUnit.Framework;
using Repository;
using Repository.DAL;
using Rhino.Mocks;

namespace Tests.ManagementCompanyTests
{
    [TestFixture, RequiresSTA]
    public class HeatSupplierViewModelTest
    {
        private IHeatSupplierRepository supplierRepository;
        private MockRepository mockRepository;
        readonly HeatSupplier[] suppliers = new HeatSupplier[1] {new HeatSupplier()};

        [SetUp]
        public void SetUp()
        {
            mockRepository = new MockRepository();
            supplierRepository = mockRepository.Stub<IHeatSupplierRepository>();
        }

        [Test]
        public void HeatSuppliers_CreateViewModel_MustFillFromRepository()
        {
            supplierRepository.Stub(x => x.GetHeatSuppliers()).Return(suppliers);
            mockRepository.ReplayAll();

            var vm = new HeatSupplierViewModel(supplierRepository);
            Assert.AreEqual(suppliers.Length, vm.HeatSuppliers.Count);
        }

        [Test]
        public void CreateSupplier_NameEmpty_DoNotCreate()
        {
            supplierRepository.Stub(x => x.GetHeatSuppliers()).Return(suppliers);
            mockRepository.ReplayAll();

            var vm = new HeatSupplierViewModel(supplierRepository);
            vm.CreateSupplierCommand.Execute(null);
            supplierRepository.AssertWasNotCalled(x => x.InsertHeatSupplier(Arg<HeatSupplier>.Is.Anything));
        }

        [Test]
        public void CreateSupplier_SetNameAndDescription_MustCreateWithAppropriatevalues()
        {
            const string name = "Any name";
            const string description = "Any description";
            supplierRepository.Stub(x => x.Save());
            supplierRepository.Stub(x => x.GetHeatSuppliers()).Return(suppliers);
            supplierRepository.Expect(
                x =>
                x.InsertHeatSupplier(Arg<HeatSupplier>.Matches(arg => arg.Name == name && arg.Description == description)));
            mockRepository.ReplayAll();

            var vm = new HeatSupplierViewModel(supplierRepository) {Name = name, Description = description};

            vm.CreateSupplierCommand.Execute(null);
            mockRepository.VerifyAll();
        }
    }
}
