﻿// -----------------------------------------------------------------------
//  <copyright file="EsentTransactionContext.cs" company="Hibernating Rhinos LTD">
//      Copyright (c) Hibernating Rhinos LTD. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Microsoft.Isam.Esent.Interop;
using Raven.Abstractions.Extensions;

namespace Raven.Database.Impl.DTC
{
	public class EsentTransactionContext : IDisposable
	{
		private readonly IntPtr sessionContext;

		public EsentTransactionContext(Session session, IntPtr context, DateTime createdAt)
		{
			sessionContext = context;
			Session = session;
			CreatedAt = createdAt;
			using (EnterSessionContext())
			{
				Transaction = new Transaction(Session);
			}

			ActionsAfterCommit = new List<Action>();
		}

		public List<Action> ActionsAfterCommit { get; private set; }
		public Session Session { get; private set; }
		public DateTime CreatedAt { get; private set; }
		public Transaction Transaction { get; private set; }

		public IDisposable EnterSessionContext()
		{
			Api.JetSetSessionContext(Session, sessionContext);

			return new DisposableAction(() => Api.JetResetSessionContext(Session));
		}

		public void AfterCommit(Action action)
		{
			ActionsAfterCommit.Add(action);
		}

		public void Dispose()
		{
			if(Transaction != null)
				Transaction.Dispose();
			if(Session != null)
				Session.Dispose();
		}
	}
}