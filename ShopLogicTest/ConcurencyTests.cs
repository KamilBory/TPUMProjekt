using Microsoft.VisualStudio.TestTools.UnitTesting;

using ShopLogic.Basic;

using System;
using System.Threading;
using System.Collections.Generic;

namespace ShopLogicTest
{
    [TestClass]
    public class ConcurencyTests
    {
        public int iterations = 1;

        private void spin(ref int v)
        {
            for (int i = 0; i < iterations; ++i)
            {
                int x = Volatile.Read(ref v);
                x += 1;
                Volatile.Write(ref v, x);
            }

            for (int i = 0; i < iterations; ++i)
            {
                int x = Volatile.Read(ref v);
                x -= 1;
                Volatile.Write(ref v, x);
            }
        }

        private void spin(ref int[] vs)
        {
            for (int i = 0; i < iterations; ++i)
            {
                for (int j = 0; j < vs.Length; ++j)
                {
                    int x = Volatile.Read(ref vs[j]);
                    x += 1;
                    Volatile.Write(ref vs[j], x);
                }
            }

            for (int i = 0; i < iterations; ++i)
            {
                for (int j = 0; j < vs.Length; ++j)
                {
                    int x = Volatile.Read(ref vs[j]);
                    x -= 1;
                    Volatile.Write(ref vs[j], x);
                }
            }
        }

        [TestMethod]
        public void ConcExec_SingleLock()
        {
            iterations = 10000000;

            object lockObj = new object();
            int i = 0;

            var t1 = new Thread(new ThreadStart(() => { Conc.LockExec(new object[] { lockObj }, () => { spin(ref i); }); }));
            var t2 = new Thread(new ThreadStart(() => { Conc.LockExec(new object[] { lockObj }, () => { spin(ref i); }); }));

            t1.Start(); t2.Start();

            t1.Join(); t2.Join();

            Assert.AreEqual(0, i);
        }

        [TestMethod]
        public void ConcExec_DoubleLock()
        {
            iterations = 800000;

            object lockObj1 = new object();
            object lockObj2 = new object();

            int[] i = new int[] { 1, 2 };

            var t1 = new Thread(new ThreadStart(() => { Conc.LockExec(new object[] { lockObj1 }, () => { spin(ref i[0]); }); }));
            var t2 = new Thread(new ThreadStart(() => { Conc.LockExec(new object[] { lockObj2 }, () => { spin(ref i[1]); }); }));
            var t3 = new Thread(new ThreadStart(() => { Conc.LockExec(new object[] { lockObj1, lockObj2 }, () => { spin(ref i); }); }));
            var t4 = new Thread(new ThreadStart(() => { Conc.LockExec(new object[] { lockObj1, lockObj2 }, () => { spin(ref i); }); }));

            t1.Start(); t2.Start(); t3.Start(); t4.Start();

            t1.Join(); t2.Join(); t3.Join(); t4.Join();

            Assert.AreEqual(1, i[0]);
            Assert.AreEqual(2, i[1]);
        }
    }
}
