﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Linq;
using NUnit.Framework;
using NHibernate.Linq;

namespace NHibernate.Test.NHSpecificTest.GH2631
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : BugTestCase
	{
		protected override void OnSetUp()
		{
			using (var session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				var person = new Person
				{
					Name = "Testing"
				};
				person.Address = new Address
				{
					Person = person,
					Street = "Mulberry"
				};
				session.Save(person);

				transaction.Commit();
			}
		}

		protected override void OnTearDown()
		{
			using (var session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				session.CreateQuery("delete from Address").ExecuteUpdate();
				session.CreateQuery("delete from System.Object").ExecuteUpdate();

				transaction.Commit();
			}
		}

		[Test]
		public async Task IndexOutOfRangeOnDeleteAsync()
		{
			using (var session = OpenSession())
			using (var t = session.BeginTransaction())
			{
				var persons = await (session.Query<Person>().ToListAsync());

				foreach (var person in persons)
				{
					await (session.DeleteAsync(person));
				}

				await (t.CommitAsync());
			}
		}

		[Test]
		public async Task UpdateAsync()
		{
			using (var session = OpenSession())
			using (var t = session.BeginTransaction())
			{
				var persons = await (session.Query<Person>().ToListAsync());

				foreach (var person in persons)
				{
					person.Name = "x";
					person.Address = null;
				}

				await (t.CommitAsync());
			}
		}
	}
}
