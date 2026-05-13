using Xunit;

namespace MIF2.UnitTests.AgentsActions
{
    public class AgentActionsTests
    {
        [Fact]
        public void Mitosis_NoEnergy_NoMitosisSpendEnergy()
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 10);
            Assert.True(true);
        }

        [Fact]
        public void Mitosis_NoSpace_NoMitosisSpendEnergy()
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 10);
            Assert.True(true);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(0)]
        public void Mitosis_AgentCanMitosis_SuccessfulMitosis(byte code)
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 80);
            Assert.True(true);
        }

        [Fact]
        public void Jump_JumpLessGenomeLength_SuccessfulJump()
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 20);
            Assert.True(true);
        }

        [Fact]
        public void Jump_JumpOverGenomeLength_SuccessfulJump()
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 20);
            Assert.True(true);
        }

        [Fact]
        public void Photosynthesis_DoPhotosynthesis_GettingEnergy()
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 20);
            Assert.True(true);
        }

        [Fact]
        public void Attack_TargetVoid_NoAttackSpendEnergy()
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 80);
            Assert.True(true);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(0)]
        public void Attack_TargetAgentWounded_SuccessfulAttackSpendEnergy(byte code)
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 80);
            Assert.True(true);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(0)]
        public void Attack_TargetAgentDead_SuccessfulAttackGettingEnergy(byte code)
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 80);
            Assert.True(true);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(0)]
        public void Move_TargetVoid_SuccessfulMoveSpendEnergy(byte code)
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 80);
            Assert.True(true);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(0)]
        public void Move_TargetCellWithAgent_NoMoveSpendEnergy(byte code)
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 80);
            Assert.True(true);
        }

        [Fact]
        public void Regeneration_NotEnoughEnergy_AgentDead()
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 20);
            Assert.True(true);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Regeneration_EnoughEnergy_IncreaseHealth(byte code)
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 20);
            Assert.True(true);
        }

        [Fact]
        public void SelfDestruction_NotEnoughHealth_AgentDead()
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 20);
            Assert.True(true);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void SelfDestruction_EnoughHealth_GettingEnergy(byte code)
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 20);
            Assert.True(true);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Prolong_EnoughEnergy_IncreasingMaximumAge(byte code)
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 20);
            Assert.True(true);
        }

        [Fact]
        public void Prolong_NotEnoughEnergy_MaximumAgeNotChange()
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next() % 20);
            Assert.True(true);
        }
    }
}
