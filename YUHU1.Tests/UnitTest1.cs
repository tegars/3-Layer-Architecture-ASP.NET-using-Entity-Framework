using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YUHU1.BLL.KelasBLL;
using AutoMapper;
using YUHU1.BLL.Kelas.MapperProfile;
namespace YUHU1.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestKelas()
        {
            Mapper.Initialize(map =>
            {
                map.AddProfile<KelasMapper>();
            });
            int a = 5;
            int b = 9;

            KelasBLL _kelasBLL = new KelasBLL();
            var c= _kelasBLL.GetAll();
        }
        [TestMethod]
        public void TestSiswa() {
            int c = 9;
        }
    }
}
