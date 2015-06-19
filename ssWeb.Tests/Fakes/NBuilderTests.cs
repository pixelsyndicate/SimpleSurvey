using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ssWeb;
using ssWeb.Models;
using ssWeb.Repositories;
using ssWeb.Tests.Helpers;


[TestClass]
public class NBuilderTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var users = GenerateUsers();

        Assert.IsNotNull(users);
        Assert.IsTrue(users.Any());
    }

    private User GenerateSingleObject()
    {
        var user1 = Builder<User>.CreateNew().Build();

        // passing constructor args
        IUserRepository userRepo = new FakeUserRepository();
        IRoleRepository roleRepo = new FakeRoleRepository();
        var selectListHelper = Builder<SelectListHelper>.CreateNew().WithConstructor(() => new SelectListHelper(userRepo, roleRepo)).Build();


        // set property values
        var user2 = Builder<User>.CreateNew().With(x => x.FirstName = "JimBob").Build();

        // set multiple property values
        var user3 = Builder<User>
            .CreateNew()
            .With(x => x.FirstName = "JimBob")
            .And(x => x.LastName = "Kempton")
            .And(x => x.ID = 0)
            .Build();

        return user1;
    }

    private IList<User> GenerateUsers()
    {
        var users = Builder<User>.CreateListOfSize(10).Build();

        // setting a value of a property for every item in the list
        var users0 = Builder<User>
            .CreateListOfSize(10)
            .All()
                .With(x => x.Role = 1)
            .Build();



        IUserRepository userRepo = new FakeUserRepository();
        IRoleRepository roleRepo = new FakeRoleRepository();

        // setting a value of a property for some items in the list
        //WhereTheFirst(x) & AndTheNext(x)
        
        
        var selector0 = Builder<SelectListHelper>
            .CreateListOfSize(4)
            .TheFirst(2)
                .WithConstructor(() => new SelectListHelper(userRepo, roleRepo))
            .AndTheNext(2)
                .WithConstructor(() => new SelectListHelper(userRepo, roleRepo))
            .Build();
        
        
        //WhereTheLast(x) & AndThePrevious(x)

        var selector1 = Builder<User>
            .CreateListOfSize(30)
            .TheLast(10)
                .With(x => x.FirstName = "JimBob")
            .ThePrevious(10)
                .With(x => x.FirstName = "Pete")
            .Build();


        //WhereRandom(x)
        var selector2 = Builder<User>.CreateListOfSize(10)
            .Random(5)
                .With(x => x.Role = 1)
                .And(x => x.Role = 2)
            .Build();


        //WhereSection(x, y)
        var selector3 = Builder<User>
           .CreateListOfSize(30)
                .WhereSection(12, 14).Have(x => x.FirstName = "JimBob")
                .Build();

        return users;
    }
}

