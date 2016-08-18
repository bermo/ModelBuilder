﻿namespace ModelBuilder.UnitTests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using NSubstitute;
    using Xunit;

    public class BuildStrategyCompilerExtensionsTests
    {
        public interface ISomeCompilerModule : ICompilerModule
        {
            // This verifies that the module scanner does not attempt to use interface modules
        }

        [Fact]
        public void AddCreationRuleAddsRuleToCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddCreationRule<DummyCreationRule>();

            var actual = target.CreationRules.Single();

            actual.Should().BeOfType<DummyCreationRule>();
        }

        [Fact]
        public void AddCreationRuleThrowsExceptionWithNullCompilerTest()
        {
            IBuildStrategyCompiler target = null;

            Action action = () => target.AddCreationRule<DummyCreationRule>();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddCreationRuleWithExpressionAddsRuleToCompilerTest()
        {
            var priority = Environment.TickCount;
            var value = Guid.NewGuid().ToString();

            var target = new BuildStrategyCompiler();

            target.AddCreationRule<Person>(x => x.FirstName, priority, value);

            var rule = target.CreationRules.Single();

            rule.Priority.Should().Be(priority);

            var actual = rule.Create(typeof(Person), nameof(Person.FirstName), null);

            actual.Should().Be(value);
        }

        [Fact]
        public void AddCreationRuleWithExpressionThrowsExceptionWithNullCompilerTest()
        {
            var priority = Environment.TickCount;
            var value = Guid.NewGuid().ToString();

            BuildStrategyCompiler target = null;

            Action action = () => target.AddCreationRule<Person>(x => x.FirstName, priority, value);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddCreationRuleWithExpressionThrowsExceptionWithNullExpressionTest()
        {
            var priority = Environment.TickCount;
            var value = Guid.NewGuid().ToString();

            var target = new BuildStrategyCompiler();

            Action action = () => target.AddCreationRule((Expression<Func<Person, object>>) null, priority, value);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddExecuteOrderRuleAddsRuleToCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddExecuteOrderRule<DummyExecuteOrderRule>();

            var actual = target.ExecuteOrderRules.Single();

            actual.Should().BeOfType<DummyExecuteOrderRule>();
        }

        [Fact]
        public void AddExecuteOrderRuleThrowsExceptionWithNullCompilerTest()
        {
            IBuildStrategyCompiler target = null;

            Action action = () => target.AddExecuteOrderRule<DummyExecuteOrderRule>();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddExecuteOrderRuleWithExpressionAddsRuleToCompilerTest()
        {
            var priority = Environment.TickCount;

            var target = new BuildStrategyCompiler();

            target.AddExecuteOrderRule<Person>(x => x.FirstName, priority);

            var rule = target.ExecuteOrderRules.Single();

            rule.Priority.Should().Be(priority);

            var actual = rule.IsMatch(typeof(Person), nameof(Person.FirstName));

            actual.Should().BeTrue();
        }

        [Fact]
        public void AddExecuteOrderRuleWithExpressionThrowsExceptionWithNullCompilerTest()
        {
            var priority = Environment.TickCount;

            BuildStrategyCompiler target = null;

            Action action = () => target.AddExecuteOrderRule<Person>(x => x.FirstName, priority);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddExecuteOrderRuleWithExpressionThrowsExceptionWithNullExpressionTest()
        {
            var priority = Environment.TickCount;

            var target = new BuildStrategyCompiler();

            Action action = () => target.AddExecuteOrderRule((Expression<Func<Person, object>>) null, priority);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddIgnoreRuleAddsRuleToCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddIgnoreRule<DummyIgnoreRule>();

            var actual = target.IgnoreRules.Single();

            actual.Should().BeOfType<DummyIgnoreRule>();
        }

        [Fact]
        public void AddIgnoreRuleThrowsExceptionWithNullCompilerTest()
        {
            IBuildStrategyCompiler target = null;

            Action action = () => target.AddIgnoreRule<DummyIgnoreRule>();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddIgnoreRuleWithExpressionAddsRuleToCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddIgnoreRule<Person>(x => x.FirstName);

            var rule = target.IgnoreRules.Single();

            rule.TargetType.Should().Be<Person>();
            rule.PropertyName.Should().Be(nameof(Person.FirstName));
        }

        [Fact]
        public void AddIgnoreRuleWithExpressionThrowsExceptionWithNullCompilerTest()
        {
            BuildStrategyCompiler target = null;

            Action action = () => target.AddIgnoreRule<Person>(x => x.FirstName);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddIgnoreRuleWithExpressionThrowsExceptionWithNullExpressionTest()
        {
            var target = new BuildStrategyCompiler();

            Action action = () => target.AddIgnoreRule((Expression<Func<Person, object>>) null);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddPostBuildActionAddsRuleToCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddPostBuildAction<DummyPostBuildAction>();

            var actual = target.PostBuildActions.Single();

            actual.Should().BeOfType<DummyPostBuildAction>();
        }

        [Fact]
        public void AddPostBuildActionThrowsExceptionWithNullCompilerTest()
        {
            IBuildStrategyCompiler target = null;

            Action action = () => target.AddPostBuildAction<DummyPostBuildAction>();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddTypeCreatorAddsRuleToCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddTypeCreator<DefaultTypeCreator>();

            var actual = target.TypeCreators.Single();

            actual.Should().BeOfType<DefaultTypeCreator>();
        }

        [Fact]
        public void AddTypeCreatorThrowsExceptionWithNullCompilerTest()
        {
            IBuildStrategyCompiler target = null;

            Action action = () => target.AddTypeCreator<DefaultTypeCreator>();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddValueGeneratorAddsRuleToCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddValueGenerator<StringValueGenerator>();

            var actual = target.ValueGenerators.Single();

            actual.Should().BeOfType<StringValueGenerator>();
        }

        [Fact]
        public void AddValueGeneratorThrowsExceptionWithNullCompilerTest()
        {
            IBuildStrategyCompiler target = null;

            Action action = () => target.AddValueGenerator<StringValueGenerator>();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddWithCreationRuleAddsRuleToCompilerTest()
        {
            var rule = new CreationRule(typeof(Person), "FirstName", Environment.TickCount, (object) null);

            var target = new BuildStrategyCompiler();

            target.Add(rule);

            target.CreationRules.Should().Contain(rule);
        }

        [Fact]
        public void AddWithCreationRuleThrowsExceptionWithNullCompilerTest()
        {
            var rule = new CreationRule(typeof(Person), "FirstName", Environment.TickCount, (object) null);

            IBuildStrategyCompiler target = null;

            Action action = () => target.Add(rule);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddWithCreationRuleThrowsExceptionWithNullRuleTest()
        {
            var target = Substitute.For<IBuildStrategyCompiler>();

            Action action = () => target.Add((CreationRule) null);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddWithExecuteOrderRuleAddsRuleToCompilerTest()
        {
            var rule = new ExecuteOrderRule(typeof(Person), "FirstName", Environment.TickCount);

            var target = new BuildStrategyCompiler();

            target.Add(rule);

            target.ExecuteOrderRules.Should().Contain(rule);
        }

        [Fact]
        public void AddWithExecuteOrderRuleThrowsExceptionWithNullCompilerTest()
        {
            var rule = new ExecuteOrderRule(typeof(Person), "FirstName", Environment.TickCount);

            IBuildStrategyCompiler target = null;

            Action action = () => target.Add(rule);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddWithExecuteOrderRuleThrowsExceptionWithNullRuleTest()
        {
            var target = Substitute.For<IBuildStrategyCompiler>();

            Action action = () => target.Add((ExecuteOrderRule) null);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddWithIgnoreRuleAddsRuleToCompilerTest()
        {
            var rule = new IgnoreRule(typeof(Person), "FirstName");

            var target = new BuildStrategyCompiler();

            target.Add(rule);

            target.IgnoreRules.Should().Contain(rule);
        }

        [Fact]
        public void AddWithIgnoreRuleThrowsExceptionWithNullCompilerTest()
        {
            var rule = new IgnoreRule(typeof(Person), "FirstName");

            IBuildStrategyCompiler target = null;

            Action action = () => target.Add(rule);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddWithIgnoreRuleThrowsExceptionWithNullRuleTest()
        {
            var target = Substitute.For<IBuildStrategyCompiler>();

            Action action = () => target.Add((IgnoreRule) null);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddWithPostBuildActionAddsRuleToCompilerTest()
        {
            var postBuildAction = new DummyPostBuildAction();

            var target = new BuildStrategyCompiler();

            target.Add(postBuildAction);

            target.PostBuildActions.Should().Contain(postBuildAction);
        }

        [Fact]
        public void AddWithPostBuildActionThrowsExceptionWithNullCompilerTest()
        {
            var postBuildAction = new DummyPostBuildAction();

            IBuildStrategyCompiler target = null;

            Action action = () => target.Add(postBuildAction);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddWithPostBuildActionThrowsExceptionWithNullRuleTest()
        {
            var target = Substitute.For<IBuildStrategyCompiler>();

            Action action = () => target.Add((IPostBuildAction) null);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddWithTypeCreatorAddsRuleToCompilerTest()
        {
            var rule = new DefaultTypeCreator();

            var target = new BuildStrategyCompiler();

            target.Add(rule);

            target.TypeCreators.Should().Contain(rule);
        }

        [Fact]
        public void AddWithTypeCreatorThrowsExceptionWithNullCompilerTest()
        {
            var rule = new DefaultTypeCreator();

            IBuildStrategyCompiler target = null;

            Action action = () => target.Add(rule);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddWithTypeCreatorThrowsExceptionWithNullRuleTest()
        {
            var target = Substitute.For<IBuildStrategyCompiler>();

            Action action = () => target.Add((ITypeCreator) null);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddWithValueGeneratorAddsRuleToCompilerTest()
        {
            var rule = new StringValueGenerator();

            var target = new BuildStrategyCompiler();

            target.Add(rule);

            target.ValueGenerators.Should().Contain(rule);
        }

        [Fact]
        public void AddWithValueGeneratorThrowsExceptionWithNullCompilerTest()
        {
            var rule = new StringValueGenerator();

            IBuildStrategyCompiler target = null;

            Action action = () => target.Add(rule);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddWithValueGeneratorThrowsExceptionWithNullRuleTest()
        {
            var target = Substitute.For<IBuildStrategyCompiler>();

            Action action = () => target.Add((IValueGenerator) null);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void RemoveCreationRuleRemovesMultipleMatchingRulesFromCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddCreationRule<DummyCreationRule>();
            target.AddCreationRule<DummyCreationRule>();
            target.AddCreationRule<DummyCreationRule>();
            target.RemoveCreationRule<DummyCreationRule>();

            target.CreationRules.Should().BeEmpty();
        }

        [Fact]
        public void RemoveCreationRuleRemovesRulesFromCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddCreationRule<DummyCreationRule>();
            target.RemoveCreationRule<DummyCreationRule>();

            target.CreationRules.Should().BeEmpty();
        }

        [Fact]
        public void RemoveCreationRuleThrowsExceptionWithNullCompilerTest()
        {
            IBuildStrategyCompiler target = null;

            Action action = () => target.RemoveCreationRule<DummyCreationRule>();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void RemoveExecuteOrderRuleRemovesMultipleMatchingRulesFromCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddExecuteOrderRule<DummyExecuteOrderRule>();
            target.AddExecuteOrderRule<DummyExecuteOrderRule>();
            target.AddExecuteOrderRule<DummyExecuteOrderRule>();
            target.RemoveExecuteOrderRule<DummyExecuteOrderRule>();

            target.ExecuteOrderRules.Should().BeEmpty();
        }

        [Fact]
        public void RemoveExecuteOrderRuleRemovesRulesFromCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddExecuteOrderRule<DummyExecuteOrderRule>();
            target.RemoveExecuteOrderRule<DummyExecuteOrderRule>();

            target.ExecuteOrderRules.Should().BeEmpty();
        }

        [Fact]
        public void RemoveExecuteOrderRuleThrowsExceptionWithNullCompilerTest()
        {
            IBuildStrategyCompiler target = null;

            Action action = () => target.RemoveExecuteOrderRule<DummyExecuteOrderRule>();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void RemoveIgnoreRuleRemovesMultipleMatchingRulesFromCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddIgnoreRule<DummyIgnoreRule>();
            target.AddIgnoreRule<DummyIgnoreRule>();
            target.AddIgnoreRule<DummyIgnoreRule>();
            target.RemoveIgnoreRule<DummyIgnoreRule>();

            target.IgnoreRules.Should().BeEmpty();
        }

        [Fact]
        public void RemoveIgnoreRuleRemovesRulesFromCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddIgnoreRule<DummyIgnoreRule>();
            target.RemoveIgnoreRule<DummyIgnoreRule>();

            target.IgnoreRules.Should().BeEmpty();
        }

        [Fact]
        public void RemoveIgnoreRuleThrowsExceptionWithNullCompilerTest()
        {
            IBuildStrategyCompiler target = null;

            Action action = () => target.RemoveIgnoreRule<DummyIgnoreRule>();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void RemovePostBuildActionRemovesMultipleMatchingRulesFromCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddPostBuildAction<DummyPostBuildAction>();
            target.AddPostBuildAction<DummyPostBuildAction>();
            target.AddPostBuildAction<DummyPostBuildAction>();
            target.RemovePostBuildAction<DummyPostBuildAction>();

            target.PostBuildActions.Should().BeEmpty();
        }

        [Fact]
        public void RemovePostBuildActionRemovesRulesFromCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddPostBuildAction<DummyPostBuildAction>();
            target.RemovePostBuildAction<DummyPostBuildAction>();

            target.PostBuildActions.Should().BeEmpty();
        }

        [Fact]
        public void RemovePostBuildActionThrowsExceptionWithNullCompilerTest()
        {
            IBuildStrategyCompiler target = null;

            Action action = () => target.RemovePostBuildAction<DummyPostBuildAction>();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void RemoveTypeCreatorRemovesMultipleMatchingRulesFromCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddTypeCreator<DefaultTypeCreator>();
            target.AddTypeCreator<DefaultTypeCreator>();
            target.AddTypeCreator<DefaultTypeCreator>();
            target.RemoveTypeCreator<DefaultTypeCreator>();

            target.TypeCreators.Should().BeEmpty();
        }

        [Fact]
        public void RemoveTypeCreatorRemovesRulesFromCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddTypeCreator<DefaultTypeCreator>();
            target.RemoveTypeCreator<DefaultTypeCreator>();

            target.TypeCreators.Should().BeEmpty();
        }

        [Fact]
        public void RemoveTypeCreatorThrowsExceptionWithNullCompilerTest()
        {
            IBuildStrategyCompiler target = null;

            Action action = () => target.RemoveTypeCreator<DefaultTypeCreator>();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void RemoveValueGeneratorRemovesMultipleMatchingRulesFromCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddValueGenerator<StringValueGenerator>();
            target.AddValueGenerator<StringValueGenerator>();
            target.AddValueGenerator<StringValueGenerator>();
            target.RemoveValueGenerator<StringValueGenerator>();

            target.ValueGenerators.Should().BeEmpty();
        }

        [Fact]
        public void RemoveValueGeneratorRemovesRulesFromCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.AddValueGenerator<StringValueGenerator>();
            target.RemoveValueGenerator<StringValueGenerator>();

            target.ValueGenerators.Should().BeEmpty();
        }


        [Fact]
        public void RemoveValueGeneratorThrowsExceptionWithNullCompilerTest()
        {
            IBuildStrategyCompiler target = null;

            Action action = () => target.RemoveValueGenerator<StringValueGenerator>();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ScanModulesPopulatesCompilerWithDetectedConfigurationTest()
        {
            var target = new BuildStrategyCompiler();

            target.ScanModules();

            target.ExecuteOrderRules.Should().ContainItemsAssignableTo<DummyExecuteOrderRule>();
            target.IgnoreRules.Should().ContainItemsAssignableTo<DummyIgnoreRule>();
            target.PostBuildActions.Should().ContainItemsAssignableTo<DummyPostBuildAction>();
            target.CreationRules.Should().ContainItemsAssignableTo<DummyCreationRule>();
            target.TypeCreators.Should().ContainItemsAssignableTo<DummyTypeCreator>();
            target.ValueGenerators.Should().ContainItemsAssignableTo<DummyValueGenerator>();
        }

        [Fact]
        public void ScanModulesThrowsExceptionWithNullCompilerTest()
        {
            var target = (IBuildStrategyCompiler) null;

            Action action = () => target.ScanModules();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void SetConstructorResolverAssignsResolverToCompilerTest()
        {
            var target = new BuildStrategyCompiler();

            target.SetConstructorResolver<DefaultConstructorResolver>();

            var actual = target.ConstructorResolver;

            actual.Should().BeOfType<DefaultConstructorResolver>();
        }

        [Fact]
        public void SetConstructorResolverThrowsExceptionWithNullCompilerTest()
        {
            IBuildStrategyCompiler target = null;

            Action action = () => target.SetConstructorResolver<DefaultConstructorResolver>();

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void SetConstructorResolverThrowsExceptionWithNullResolverTest()
        {
            var target = Substitute.For<IBuildStrategyCompiler>();

            Action action = () => target.SetConstructorResolver(null);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void SetConstructorResolverWithResolverAssignsResolverToCompilerTest()
        {
            var resolver = new DefaultConstructorResolver();

            var target = new BuildStrategyCompiler();

            target.SetConstructorResolver(resolver);

            target.ConstructorResolver.Should().Be(resolver);
        }

        [Fact]
        public void SetConstructorResolverWithResolverThrowsExceptionWithNullCompilerTest()
        {
            var resolver = new DefaultConstructorResolver();

            IBuildStrategyCompiler target = null;

            Action action = () => target.SetConstructorResolver(resolver);

            action.ShouldThrow<ArgumentNullException>();
        }

        public abstract class AbstractCompilerModule : ICompilerModule
        {
            public void Configure(IBuildStrategyCompiler compiler)
            {
                // This verifies that the module scanner does not attempt to use abstract modules
                throw new NotImplementedException();
            }
        }
    }
}