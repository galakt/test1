﻿using System;
using System.ServiceModel;
using Microsoft.Practices.Unity;

namespace ConsoleAppK.DI.Wcf
{
    public class UnityServiceHost : ServiceHost
    {
        public UnityContainer Container { private set; get; }

        public UnityServiceHost()
            : base()
        {
            Container = new UnityContainer();
        }

        public UnityServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            Container = new UnityContainer();
        }

        protected override void OnOpening()
        {
            if (this.Description.Behaviors.Find<UnityServiceBehavior>() == null)
            {
                this.Description.Behaviors.Add(new UnityServiceBehavior(Container));
            }
            base.OnOpening();
        }
    }
}
