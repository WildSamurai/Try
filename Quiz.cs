using System;
using System.Collections.Generic;

class QuizProgram
{
    
    struct Question
    {
        public string Text;
        public List<string> Options;
        public int CorrectAnswerIndex;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Quiz!");
        Console.WriteLine("Answer the questions by selecting the correct option.\n");

        
        List<Question> questions = CreateQuestions();

        
        int score = RunQuiz(questions);

       
        ShowResult(score, questions.Count);

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

   
    static List<Question> CreateQuestions()
    {
        var questions = new List<Question>();

        
        questions.Add(new Question
        {
            Text = "What is the capital of France?",
            Options = new List<string> { "1. Berlin", "2. Madrid", "3. Paris", "4. Rome" },
            CorrectAnswerIndex = 2
        });

       
        questions.Add(new Question
        {
            Text = "Largest planet in our Solar System?",
            Options = new List<string> { "1. Earth", "2. Jupiter", "3. Saturn", "4. Mars" },
            CorrectAnswerIndex = 1
        });

       
        questions.Add(new Question
        {
            Text = "2 + 2 × 2 = ?",
            Options = new List<string> { "1. 6", "2. 8", "3. 4", "4. 10" },
            CorrectAnswerIndex = 0
        });

       
        questions.Add(new Question
        {
            Text = "Author of 'War and Peace'?",
            Options = new List<string> { "1. Dostoevsky", "2. Pushkin", "3. Tolstoy", "4. Chekhov" },
            CorrectAnswerIndex = 2
        });

        
        questions.Add(new Question
        {
            Text = "Chemical symbol for gold?",
            Options = new List<string> { "1. Go", "2. Gd", "3. Au", "4. Ag" },
            CorrectAnswerIndex = 2
        });

        return questions;
    }

    
    static int RunQuiz(List<Question> questions)
    {
        int score = 0;

        for (int i = 0; i < questions.Count; i++)
        {
            Question currentQuestion = questions[i];

            Console.WriteLine($"Question {i + 1}: {currentQuestion.Text}");

            
            foreach (string option in currentQuestion.Options)
            {
                Console.WriteLine(option);
            }

           
            int userAnswer = GetUserAnswer(currentQuestion.Options.Count);

            
            if (userAnswer == currentQuestion.CorrectAnswerIndex)
            {
                Console.WriteLine("Correct!\n");
                score++;
            }
            else
            {
                Console.WriteLine($"Wrong! Correct answer: {currentQuestion.Options[currentQuestion.CorrectAnswerIndex]}\n");
            }
        }

        return score;
    }

    
    static int GetUserAnswer(int optionsCount)
    {
        while (true)
        {
            Console.Write("Your answer (enter number): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int answer))
            {
                
                int index = answer - 1;

                if (index >= 0 && index < optionsCount)
                {
                    return index;
                }
            }

            Console.WriteLine($"Please enter a number from 1 to {optionsCount}");
        }
    }

    
    static void ShowResult(int score, int totalQuestions)
    {
        
        Console.WriteLine("QUIZ RESULTS");
        
        Console.WriteLine($"Correct answers: {score} out of {totalQuestions}");

        double percentage = (double)score / totalQuestions * 100;
        Console.WriteLine($"Success rate: {percentage:F1}%");

       
        if (percentage >= 90)
            Console.WriteLine("Excellent!");
        else if (percentage >= 70)
            Console.WriteLine("Good job!");
        else if (percentage >= 50)
            Console.WriteLine("Not bad!");
        else
            Console.WriteLine("Try again!");
    }
}