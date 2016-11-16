﻿namespace ModelBuilder
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The <see cref="BuildStrategy" />
    ///     class is used to provide a basic build strategy.
    /// </summary>
    public class BuildStrategy : BuildStrategyBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BuildStrategyBase" /> class.
        /// </summary>
        /// <param name="constructorResolver">The constructor resolver.</param>
        /// <param name="creationRules">The creation rules.</param>
        /// <param name="typeCreators">The type creators.</param>
        /// <param name="valueGenerators">The value generators.</param>
        /// <param name="ignoreRules">The ignore rules.</param>
        /// <param name="executeOrderRules">The execute order rules.</param>
        /// <param name="postBuildActions">The post-build actions.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="constructorResolver" /> parameter is null.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="creationRules" /> parameter is null.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="typeCreators" /> parameter is null.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="valueGenerators" /> parameter is null.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="ignoreRules" /> parameter is null.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="executeOrderRules" /> parameter is null.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="postBuildActions" /> parameter is null.</exception>
        public BuildStrategy(
            IConstructorResolver constructorResolver,
            IEnumerable<CreationRule> creationRules,
            IEnumerable<ITypeCreator> typeCreators,
            IEnumerable<IValueGenerator> valueGenerators,
            IEnumerable<IgnoreRule> ignoreRules,
            IEnumerable<ExecuteOrderRule> executeOrderRules,
            IEnumerable<IPostBuildAction> postBuildActions)
            : base(
                constructorResolver,
                creationRules,
                typeCreators,
                valueGenerators,
                ignoreRules,
                executeOrderRules,
                postBuildActions)
        {
        }

        /// <inheritdoc />
        public override IBuildLog GetBuildLog()
        {
            return new DefaultBuildLog();
        }

        /// <inheritdoc />
        public override IExecuteStrategy<T> GetExecuteStrategy<T>()
        {
            return this.With<DefaultExecuteStrategy<T>>();
        }
    }
}