using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    const string ConfigurationDataFileName = "ConfigurationData.csv";

    // configuration data
    static float paddleMoveUnitsPerSecond = 10;
    static float ballImpulseForce = 250;
    static float ballLifetime = 10;
    static float ballStartDelay = 1;
    static float minSpawnTime = 5;
    static float maxSpawnTime = 10;
    static int standardBlockPoints = 1;
    static int bonusBlockPoints = 2;
    static int pickupBlockPoints = 5;
    static float standardBlockProb = 0.7f;
    static float bonusBlockProb = 0.2f;
    static float pickupBlockProb = 0.05f;
    static int ballsPerGame = 10;
    static float freezeDuration = 2;
    static float speedupDuration = 2;
    static float ballSpeedupFactor = 2;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public float PaddleMoveUnitsPerSecond
    {
        get { return paddleMoveUnitsPerSecond; }
    }

    /// <summary>
    /// Gets the impulse force to apply to move the ball
    /// </summary>
    /// <value>impulse force</value>
    public float BallImpulseForce
    {
        get { return ballImpulseForce; }    
    }

    public float BallLifetime
    {
        get { return ballLifetime; }
    }

    public float BallStartDelay
    {
        get { return ballStartDelay; }
    }

    public float MinSpawnTime
    {
        get { return minSpawnTime; }
    }

    public float MaxSpawnTime
    {
        get { return maxSpawnTime; }
    }

    public int StandardBlockPoints
    {
        get { return standardBlockPoints; }
    }

    public int BonusBlockPoints
    {
        get { return bonusBlockPoints; }
    }

    public int PickupBlockPoints
    {
        get { return pickupBlockPoints; }
    }

    public float StandardBlockProb
    {
        get { return standardBlockProb; }
    }

    public float BonusBlockProb
    {
        get { return bonusBlockProb; }
    }

    public float PickupBlockProb
    {
        get { return pickupBlockProb; }
    }

    public int BallsPerGame
    {
        get { return ballsPerGame; }
    }

    public float FreezeDuration
    {
        get { return freezeDuration; }
    }

    public float SpeedupDuration
    {
        get { return speedupDuration; }
    }

    public float BallSpeedupFactor
    {
        get { return ballSpeedupFactor; }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        // read and save configuration data from file
        StreamReader reader = null;
        try
        {
            reader = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));
            
            string names = reader.ReadLine();
            string values = reader.ReadLine();
            SetConfigurationDataFields(values);
        } catch (Exception e)
        {
            // do nothing
        } finally
        {
            if (reader != null) reader.Close();
        }
    }

    private void SetConfigurationDataFields(string csvValues)
    {
        var values = csvValues.Split(',');
        paddleMoveUnitsPerSecond = float.Parse(values[0]);
        ballImpulseForce = float.Parse(values[1]);
        ballLifetime = float.Parse(values[2]);
        ballStartDelay = float.Parse(values[3]);
        minSpawnTime = float.Parse(values[4]);
        maxSpawnTime = float.Parse(values[5]);
        standardBlockPoints = int.Parse(values[6]);
        bonusBlockPoints = int.Parse(values[7]);
        pickupBlockPoints = int.Parse(values[8]);
        standardBlockProb = float.Parse(values[9]);
        bonusBlockProb = float.Parse(values[10]);
        pickupBlockProb = float.Parse(values[11]);
        ballsPerGame = int.Parse(values[12]);
        freezeDuration = float.Parse(values[13]);
        speedupDuration = float.Parse(values[14]);
        ballSpeedupFactor = float.Parse(values[15]);
    }

    #endregion
}
