﻿using FluentAssertions;
using System;
using Xunit;

namespace Minesweeper.Tests
{
    public sealed class DroneTests
    {
        [Fact]
        public void TestDroneInitialization()
        {
            var drone = new Drone(10, 5, 0, 0, FacingDirection.East);
            drone.XLimit.Should().Be(10);
            drone.YLimit.Should().Be(5);
            drone.X.Should().Be(0);
            drone.Y.Should().Be(0);
            drone.Direction.Should().Be(FacingDirection.East);

            drone = new Drone(10, 20, 5, 8, FacingDirection.North);
            drone.XLimit.Should().Be(10);
            drone.YLimit.Should().Be(20);
            drone.X.Should().Be(5);
            drone.Y.Should().Be(8);
            drone.Direction.Should().Be(FacingDirection.North);
        }

        [Fact]
        public void TestDroneBadInitialization()
        {
            Action act = () => new Drone(-1, 10, 0, 0);
            act.ShouldThrow<ArgumentOutOfRangeException>().And.ParamName.Should().Be("xlimit");
            act = () => new Drone(10, -1, 0, 0);
            act.ShouldThrow<ArgumentOutOfRangeException>().And.ParamName.Should().Be("ylimit");
            act = () => new Drone(10, 10, -1, 0);
            act.ShouldThrow<ArgumentOutOfRangeException>()
               .And.Message.Should().Match("X position must be greater or equal to 0 and less or equal to XLimit.*Parameter name: x");
            act = () => new Drone(10, 10, 11, 0);
            act.ShouldThrow<ArgumentOutOfRangeException>()
               .And.Message.Should().Match("X position must be greater or equal to 0 and less or equal to XLimit.*Parameter name: x");
            act = () => new Drone(10, 10, 0, -1);
            act.ShouldThrow<ArgumentOutOfRangeException>()
               .And.Message.Should().Match("Y position must be greater or equal to 0 and less or equal to YLimit.*Parameter name: y");
            act = () => new Drone(10, 10, 10, 11);
            act.ShouldThrow<ArgumentOutOfRangeException>()
               .And.Message.Should().Match("Y position must be greater or equal to 0 and less or equal to YLimit.*Parameter name: y");
        }

        [Fact]
        public void TestDroneLimitInitialization()
        {
            Action act = () => new Drone(100, 250, 0, 0);
            act.ShouldNotThrow<ArgumentOutOfRangeException>();
            act = () => new Drone(100, 250, 100, 250);
            act.ShouldNotThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void TestDroneLeftSpins()
        {
            FacingDirection[] expectedDirections = 
            {
                FacingDirection.North,
                FacingDirection.West,
                FacingDirection.South,
                FacingDirection.East
            };

            TestDroneSpins(expectedDirections, d => d.SpinLeft());
        }

        [Fact]
        public void TestDroneRightSpins()
        {
            FacingDirection[] expectedDirections =
            {
                FacingDirection.South,
                FacingDirection.West,
                FacingDirection.North,
                FacingDirection.East
            };

            TestDroneSpins(expectedDirections, d => d.SpinRight());
        }

        private void TestDroneSpins(FacingDirection[] expectedDirections, Func<Drone,Drone> spinHandler)
        {
            var currentDrone = new Drone(100, 100, 50, 50);
            currentDrone.Direction.Should().Be(FacingDirection.East);

            for (int i = 0; i < expectedDirections.Length; i++)
            {
                Drone rotatedDrone = spinHandler(currentDrone);
                AssertSameDronePositionsAndLimits(currentDrone, rotatedDrone);
                rotatedDrone.Direction.Should().Be(expectedDirections[i]);

                currentDrone = rotatedDrone;
            }
        }

        private void AssertSameDronePositionsAndLimits(Drone d1, Drone d2)
        {
            AssertSameDroneLimits(d1, d2);
            AssertSameDronePositions(d1, d2);
        }

        private void AssertSameDroneLimits(Drone d1, Drone d2)
        {
            d1.XLimit.Should().Be(d2.XLimit);
            d1.YLimit.Should().Be(d2.YLimit);
        }

        private void AssertSameDronePositions(Drone d1, Drone d2)
        {
            d1.X.Should().Be(d2.X);
            d1.Y.Should().Be(d2.Y);
        }

        [Fact]
        public void TestMoveForwardEast()
        {
            var drone = new Drone(100, 100, 50, 50, FacingDirection.East);
            
            Drone forwardedDrone = drone.MoveForward();
            AssertSameDroneLimits(drone, forwardedDrone);
            forwardedDrone.Y.Should().Be(drone.Y);
            forwardedDrone.X.Should().Be(drone.X + 1);
        }

        [Fact]
        public void TestMoveForwardWest()
        {
            var drone = new Drone(100, 100, 50, 50, FacingDirection.West);
            
            Drone forwardedDrone = drone.MoveForward();
            AssertSameDroneLimits(drone, forwardedDrone);
            forwardedDrone.Y.Should().Be(drone.Y);
            forwardedDrone.X.Should().Be(drone.X - 1);
        }

        [Fact]
        public void TestMoveForwardNorth()
        {
            var drone = new Drone(100, 100, 50, 50, FacingDirection.North);

            Drone forwardedDrone = drone.MoveForward();
            AssertSameDroneLimits(drone, forwardedDrone);
            forwardedDrone.Y.Should().Be(drone.Y + 1);
            forwardedDrone.X.Should().Be(drone.X);
        }

        [Fact]
        public void TestMoveForwardSouth()
        {
            var drone = new Drone(100, 100, 50, 50, FacingDirection.South);

            Drone forwardedDrone = drone.MoveForward();
            AssertSameDroneLimits(drone, forwardedDrone);
            forwardedDrone.Y.Should().Be(drone.Y - 1);
            forwardedDrone.X.Should().Be(drone.X);
        }

        [Fact]
        public void TestMoveForwardAtLimit()
        {
            Drone[] limitDrones = new Drone[]
            {
                new Drone(100, 100, 100, 50, FacingDirection.East),
                new Drone(100, 100, 0, 50, FacingDirection.West),
                new Drone(100, 100, 50, 100, FacingDirection.North),
                new Drone(100, 100, 50, 0, FacingDirection.South)
            };

            bool res = Array.TrueForAll(limitDrones, d => !d.CanMoveForward);
            res.Should().BeTrue();

            res = Array.TrueForAll(limitDrones, d =>
            {
                Drone forwardedDrone = d.MoveForward();
                return ReferenceEquals(d, forwardedDrone);
            });
            res.Should().BeTrue();
        }

        [Fact]
        public void TestToString()
        {
            var drone = new Drone(100, 100, 50, 50, FacingDirection.South);
            drone.ToString().Should().Be("50 50 S");
            drone = new Drone(100, 100, 25, 35, FacingDirection.North);
            drone.ToString().Should().Be("25 35 N");
            drone = new Drone(100, 100, 10, 15, FacingDirection.West);
            drone.ToString().Should().Be("10 15 W");
            drone = new Drone(100, 100, 5, 8, FacingDirection.East);
            drone.ToString().Should().Be("5 8 E");
        }
    }
}
