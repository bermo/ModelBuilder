﻿namespace ModelBuilder
{
    using System;
    using ModelBuilder.Data;

    /// <summary>
    ///     The <see cref="LastNameValueGenerator" />
    ///     class is used to generate random last name values.
    /// </summary>
    public class LastNameValueGenerator : RelativeValueGenerator
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LastNameValueGenerator" />.
        /// </summary>
        public LastNameValueGenerator()
            : base(PropertyExpression.LastName, typeof(string))
        {
        }

        /// <inheritdoc />
        protected override object GenerateValue(Type type, string referenceName, IExecuteStrategy executeStrategy)
        {
            return TestData.LastNames.Next();
        }

        /// <inheritdoc />
        public override int Priority { get; } = 1000;
    }
}