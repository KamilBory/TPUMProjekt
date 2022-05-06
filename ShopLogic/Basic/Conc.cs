using System.Threading;

namespace ShopLogic.Basic
{
    static public class Conc
    {
        public delegate T Executor<T>();
        public delegate void Executor();

        public static T LockExec<T>(object[] locks, Executor<T> executor)
        {
            for (int i = 0; i < locks.Length; ++i)
            {
                Monitor.Enter(locks[i]);
            }
            try
            {
                return executor();
            }
            finally
            {
                for (int i = locks.Length - 1; i >= 0; --i)
                {
                    Monitor.Exit(locks[i]);
                }
            }
        }

        public static void LockExec(object[] locks, Executor executor)
        {
            for (int i = 0; i < locks.Length; ++i)
            {
                Monitor.Enter(locks[i]);
            }
            try
            {
                executor();
            }
            finally
            {
                for (int i = locks.Length - 1; i >= 0; --i)
                {
                    Monitor.Exit(locks[i]);
                }
            }
        }
    }
}
