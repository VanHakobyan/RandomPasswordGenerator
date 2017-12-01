namespace RNGcriptoClassUsing
{
    using System;
	using NUnit.Framework;

    [TestFixture]
	public class PasswordGeneratorFixture
	{
		public PasswordGeneratorFixture()
		{
		}

		[SetUp]
		public void Init()
		{
			defaultPwdGen   = new PasswordGenerator();
			customPwdGen    = new PasswordGenerator();

			customPwdGen.Minimum 				= 8;
			customPwdGen.Maximum				= 15;
            customPwdGen.ConsecutiveCharacters 	= false;
			customPwdGen.RepeatCharacters		= false;

			defaultPassword 					= null;
            customPassword   					= null;
		}
        
		[TearDown]
		public void Destroy()
		{
			defaultPwdGen 	= null;
			customPwdGen 	= null;
			defaultPassword	= null;
            customPassword	= null;
		}

        [Test]
        public void Default()
        {
            defaultPassword = defaultPwdGen.Generate();
            Assertion.AssertEquals("Minimum should be set to default value.", 6, defaultPwdGen.Minimum);
            Assertion.AssertEquals("Maximum should be set to default value.", 10, defaultPwdGen.Maximum);
            Assertion.AssertEquals("Consecutive characters are not allowed by default.", false, defaultPwdGen.ConsecutiveCharacters);
            Assertion.AssertEquals("Repeating characters are  allowed by default.", true, defaultPwdGen.RepeatCharacters);
            Assertion.Assert(String.Empty != defaultPassword);
        }

        [Test]
        public void DefaultMinimum()
        {
            PasswordGenerator min = new PasswordGenerator();
            min.Minimum = 0;
            Assertion.AssertEquals("Minimum should be set to default value if property value is lesser.", 6, min.Minimum);
        }

        [Test]
        public void DefaultMaximum()
        {
            PasswordGenerator max = new PasswordGenerator();
            max.Minimum = 0;
            max.Maximum = 0;
            Assertion.AssertEquals("Maximum should be set to default value if property value is lesser.", 10, max.Maximum);
        }

		[Test]
        public void PasswordNotEmpty()
		{
			customPassword = customPwdGen.Generate();
            Assertion.Assert(String.Empty != customPassword);
		}

		[Test]
		public void SizeConstraints()
		{
            customPassword = customPwdGen.Generate();
            Assertion.Assert(customPassword.Length >= customPwdGen.Minimum);
			Assertion.Assert(customPassword.Length <= customPwdGen.Maximum);
		}

		[Test]
		public void ConsecutiveCharacterConstraints()
		{
            customPassword = customPwdGen.Generate();
            
            bool hasConsecutive = false;
            for ( int i = 0; i < customPassword.Length - 1; i++ )
            {
                if ( customPassword[i] == customPassword[i+1] )
                {
                    hasConsecutive = true;
                }
            }
            Assertion.Assert(!(customPwdGen.ConsecutiveCharacters && hasConsecutive));
		}

		[Test]
		public void RepeatingCharacterConstraints()
		{
            customPassword = customPwdGen.Generate();
            
            bool hasRepeating = false;
            for ( int i = 0; i < customPassword.Length - 1; i++ )
            {
                if ( -1 != customPassword.IndexOf(customPassword[i], i+1 ) )
                {
                    hasRepeating = true;
                }
            }
            Assertion.Assert(!(customPwdGen.RepeatCharacters && hasRepeating));
        }
        
        [Test]
        public void ExcludeSymbols()
        {
            customPwdGen.ExcludeSymbols = true;
            customPassword = customPwdGen.Generate();
            foreach ( char item in customPassword )
            {
                Assertion.Assert("No symbols should be in the resulting password.", Char.IsLetterOrDigit(item));
            }        
        }

        [Test]
        public void ExcludeCharacters()
        {
            string exclude = "0123456789";
            customPwdGen.Exclusions = exclude;
            customPassword = customPwdGen.Generate();
            foreach ( char item in exclude )
            {
                Assertion.AssertEquals("No character in the exclusion set should be in the resulting password.", -1, customPassword.IndexOf(item));
            }
        }

		private PasswordGenerator defaultPwdGen, customPwdGen;
		private string defaultPassword, customPassword;
	}
}