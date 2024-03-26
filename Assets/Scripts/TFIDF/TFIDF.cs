using Accord.MachineLearning;
using Accord.Statistics.Analysis;
using UnityEngine;
delegate double[][] TextAnalysisMethod(string[] texts);
public static class TextAnalysis
{
    private static void Keke()
    {
        string[] texts =
        {
            @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas molestie malesuada 
            nisi et placerat. Curabitur blandit porttitor suscipit. Nunc facilisis ultrices felis,
            vitae luctus arcu semper in. Fusce ut felis ipsum. Sed faucibus tortor ut felis placerat
            euismod. Vestibulum pharetra velit et dolor ornare quis malesuada leo aliquam. Aenean 
            lobortis, tortor iaculis vestibulum dictum, tellus nisi vestibulum libero, ultricies 
            pretium nisi ante in neque. Integer et massa lectus. Aenean ut sem quam. Mauris at nisl 
            augue, volutpat tempus nisl. Suspendisse luctus convallis metus, vitae pretium risus 
            pretium vitae. Duis tristique euismod aliquam",

            @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas molestie malesuada 
            nisi et placerat. Curabitur blandit porttitor suscipit. Nunc facilisis ultrices felis,
            vitae luctus arcu semper in. Fusce ut felis ipsum. Sed faucibus tortor ut felis placerat
            euismod. Vestibulum pharetra velit et dolor ornare quis malesuada leo aliquam. Aenean 
            lobortis, tortor iaculis vestibulum dictum, tellus nisi vestibulum libero, ultricies 
            pretium nisi ante in neque. Integer et massa lectus. Aenean ut sem quam. Mauris at nisl 
            augue, volutpat tempus nisl. Suspendisse luctus convallis metus, vitae pretium risus 
            pretium vitae. Duis tristique euismod aliquam",

            @"Sed consectetur nisl et diam mattis varius. Aliquam ornare tincidunt arcu eget adipiscing. 
            Etiam quis augue lectus, vel sollicitudin lorem. Fusce lacinia, leo non porttitor adipiscing, 
            mauris purus lobortis ipsum, id scelerisque erat neque eget nunc. Suspendisse potenti. Etiam 
            non urna non libero pulvinar consequat ac vitae turpis. Nam urna eros, laoreet id sagittis eu,
            posuere in sapien. Phasellus semper convallis faucibus. Nulla fermentum faucibus tellus in 
            rutrum. Maecenas quis risus augue, eu gravida massa."
        };

        // Debug.Log("hehe");
        string[][] words = texts.Tokenize();

        // Create a new TF-IDF with options:
        var codebook = new TFIDF()
        {
            Tf = TermFrequency.Log,
            Idf = InverseDocumentFrequency.Default
        };

        // Compute the codebook (note: this would have to be done only for the training set)
        codebook.Learn(words);

        // Now, we can use the learned codebook to extract fixed-length
        // representations of the different texts (paragraphs) above:

        double[][] tfidfVectors = new double[words.Length][];
        for (int i = 0; i < words.Length; i++)
        {
            tfidfVectors[i] = codebook.Transform(words[i]);
        }
        
        PrincipalComponentAnalysis PCA = new PrincipalComponentAnalysis(PrincipalComponentMethod.Center);
        PCA.Learn(tfidfVectors);
        PCA.NumberOfOutputs = 3;
        var result = PCA.Transform(tfidfVectors);
    }

    public static double[][] AnalysisBOW(string[] texts)
    {
        string[][] words = texts.Tokenize();

        // Create a new TF-IDF with options:
        var codebook = new BagOfWords()
        {
            // the resulting vector will have only 0's and 1's
        };

        // Compute the codebook (note: this would have to be done only for the training set)
        codebook.Learn(words);


        double[][] bowVectors = new double[words.Length][];
        for (int i = 0; i < words.Length; i++)
        {
            bowVectors[i] = codebook.Transform(words[i]);
        }
        
        PrincipalComponentAnalysis PCA = new PrincipalComponentAnalysis(PrincipalComponentMethod.Center);
        PCA.Learn(bowVectors);
        PCA.NumberOfOutputs = 3;
        var result = PCA.Transform(bowVectors);
        return result;
    }

    

    public static double[][] AnalysisTFIDF(string[] texts)
    {
        string[][] words = texts.Tokenize();

        // Create a new TF-IDF with options:
        var codebook = new TFIDF()
        {
            Tf = TermFrequency.Log,
            Idf = InverseDocumentFrequency.Default
        };

        // Compute the codebook (note: this would have to be done only for the training set)
        codebook.Learn(words);

        // Now, we can use the learned codebook to extract fixed-length
        // representations of the different texts (paragraphs) above:

        double[][] tfidfVectors = new double[words.Length][];
        for (int i = 0; i < words.Length; i++)
        {
            tfidfVectors[i] = codebook.Transform(words[i]);
        }
        
        PrincipalComponentAnalysis PCA = new PrincipalComponentAnalysis(PrincipalComponentMethod.Center);
        PCA.Learn(tfidfVectors);
        PCA.NumberOfOutputs = 3;
        var result = PCA.Transform(tfidfVectors);

        return result;
    }
}
