using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class CsvLoader
{
    
    public static List<SequenceObjectFile> LoadingFromFile(string pathFromStreamingAssets)
    {
        
        // Instanciting the sequences list
        List<SequenceObjectFile> sequenceObjectFiles = new List<SequenceObjectFile>();

        // Loading the sequence file
        string path = Application.streamingAssetsPath + pathFromStreamingAssets;
        string cvsFile = File.ReadAllText(path);
        String[] lines = cvsFile.Split(System.Environment.NewLine[0]);
        // Looping on lines of the file
        // KEEP ATTENTION:
        // - line starts from 1 in order to skip the header line
        // - line arrives before lines.Lenght - 1 because the last line is empty
        for (int line = 1; line < lines.Length - 1; line++)
        {
            // Parsing the file removing the separetors
            String[] lineData = lines[line].Split(","[0]);

            // Creating the Unity Object representing the sequences
            SequenceObjectFile objectFile = new SequenceObjectFile();
            objectFile.sequences = new List<List<int>>();

            // Converting the difficulty
            objectFile.difficulty = int.Parse(lineData[0]);

            // Retriving all the sequences of the target difficulty
            for (int seqID = 1; seqID < lineData.Length; seqID++)
            {
                // Creating the list of a given sequence
                List<int> seqList = new List<int>();

                // Looping on chars to add numbers in the list
                foreach (char singleChar in lineData[seqID])
                {
                    if (singleChar != " "[0])
                        seqList.Add(int.Parse(singleChar.ToString()));
                }

                // Adding the sequence to the sequences' list
                objectFile.sequences.Add(seqList);
            }

            // Adding the Object in the Class list
            sequenceObjectFiles.Add(objectFile);
        }

        return sequenceObjectFiles;
    }

}

public class SequenceObjectFile
{
    public int difficulty;
    public List<List<int>> sequences;
}
