using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    public static ConfigurationData ConfigurationData;

    #region Properties
    
    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public static float PaddleMoveUnitsPerSecond
    {
        get { return ConfigurationData.PaddleMoveUnitsPerSecond; }
    }

    public static float BallImpulseForce
    {
        get { return ConfigurationData.BallImpulseForce; }
    }

    public static float BallLifetime
    {
        get { return ConfigurationData.BallLifetime; }
    }

    public static float BallStartDelay
    {
        get { return ConfigurationData.BallStartDelay; }
    }

    public static float MinSpawnTime
    {
        get { return ConfigurationData.MinSpawnTime; }
    }

    public static float MaxSpawnTime
    {
        get { return ConfigurationData.MaxSpawnTime; }
    }

    public static int StandardBlockPoints
    {
        get { return ConfigurationData.StandardBlockPoints; }
    }

    public static int BonusBlockPoints
    {
        get { return ConfigurationData.BonusBlockPoints; }
    }

    public static int PickupBlockPoints
    {
        get { return ConfigurationData.PickupBlockPoints; }
    }

    public static float StandardBlockProb
    {
        get { return ConfigurationData.StandardBlockProb; }
    }

    public static float BonusBlockProb
    {
        get { return ConfigurationData.BonusBlockProb; }
    }

    public static float PickupBlockProb
    {
        get { return ConfigurationData.PickupBlockProb; }
    }

    public static int BallsPerGame
    {
        get { return ConfigurationData.BallsPerGame; }
    }

    public static float FreezeDuration
    {
        get { return ConfigurationData.FreezeDuration; }
    }

    public static float SpeedupDuration
    {
        get { return ConfigurationData.SpeedupDuration; }
    }

    public static float BallSpeedupFactor
    {
        get { return ConfigurationData.BallSpeedupFactor; }
    }
    #endregion

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {
        ConfigurationData = new ConfigurationData();
    }
}
