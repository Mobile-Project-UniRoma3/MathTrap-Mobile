﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MathTrap
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
        {
            
            InitializeComponent();
            
            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "it": this.label1.Text = "Benvenuti in MathTrap!!";
                           this.textcell1.Text="Regole di gioco: MathTrap si basa sulla soluzione delle quattro operazioni fondamentali dell'aritmetica (somma, sottrazione, moltiplicazione e divisione). " +
                                               "E' previsto dopo un numero prestabilito di operazioni risolte(dieci), un operazione bonus tra la soluzione di una potenza o di una radice quadrata, che regalerà ogni volta che verrà risolta un punto vita, che si accumulerà a quelli presenti(ad ogni nuova partita il giocatore avrà a disposizione 5 punti vita). " +
                                               "Per ogni operazione risolta si incrementerà un contatore di risposte esatte, mentre per ogni risposta sbagliata verra incrementato in contatore di risposte sbagliate e in più verra tolta una vita tra quelle a disposizione(quando le vite terminano, terminerà anche la partita). " +
                                               "Se l'operazione sarà considerata troppo difficile da risolvere si potrà usare il tasto skip, che permette di saltare all'operazione successiva senza influire sul punteggio. " +
                                               "Approposito della divisione per quanto riguarda il risultato che il giocatore dovrà fornire, questo dovrà essere fornito con due decimali, anche quando il risultato sarà intero(es: 2.34; 3.00). ";
                                               break;
                case "fr": this.label1.Text = "Bienvenu en MathTrap!!";
                           this.textcell1.Text="Regles du jeu: MathTrap est basé sur la résolution des quatre opérations fondamentales de l'arithmétique (addition, soustraction, multiplication et division). " +
                                               "après un nombre prédéterminé d'opérations résolues (dix), une opération bonus entre la solution d'une puissance ou d'une racine carrée, qui donnera à chaque fois un point de vie résolu, qui s'accumulera aux présents(à chaque nouvelle partie le joueur ont 5 points de vie disponibles). " +
                                               "Pour chaque opération résolue, un compteur de bonnes réponses sera augmenté, tandis que pour chaque mauvaise réponse, le compteur de mauvaises réponses sera augmenté et une vie sera retirée(lorsque les vies se terminent, le jeu se termine également). " +
                                               "Si l'opération est considérée comme trop difficile à résoudre, vous pouvez utiliser le bouton de saut, qui vous permet de passer à l'opération suivante sans affecter le score. " +
                                               "Concernant la division quant au résultat que le joueur doit fournir, celui-ci doit être donné avec deux décimales, même lorsque le résultat est entier (ex: 2, 34 ; 3,00).";
                                               break;
                case "de": this.label1.Text = "Willkommen zu MathTrap!!";
                    this.textcell1.Text="Spielregeln: MathTrap basiert auf der Lösung der vier Grundrechenarten(Addition, Subtraktion, Multiplikation und Division). " +
                                        "nach einer vorgegebenen Anzahl von ausgeführten Operationen(zehn), eine Bonusoperation zwischen der Lösung einer Potenz oder einer Quadratwurzel, die jedes Mal einen Lebenspunkt ergibt, der den Anwesenden angesammelt wird(bei jedem neuen Spiel wird der Spieler 5 Lebenspunkte zur Verfügung haben). " +
                                        "Für jede gelöste Operation wird ein Zähler für richtige Antworten erhöht, während für jede falsche Antwort der Zähler für falsche Antworten erhöht wird und ein Leben weggenommen wird(wenn die Leben enden, endet auch das Spiel). " +
                                        "Wenn die Lösung der Operation als zu schwierig erachtet wird, können Sie die Schaltfläche „Überspringen“ verwenden, mit der Sie zur nächsten Operation springen können, ohne die Partitur zu beeinflussen. " +
                                        "Was die Division des vom Spieler zu liefernden Ergebnisses betrifft, so muss dieses mit zwei Dezimalstellen angegeben werden, auch wenn das Ergebnis ganzzahlig ist (z.B.: 2, 34; 3,00).";
                                        break;
                case "cn": this.label1.Text = "歡迎來到數學陷阱 ! !";
                    this.textcell1.Text="遊戲規則 MathTrap 基於算術的四種基本運算（加法、減法、乘法和除法）的解決方案。" +
                                        "在預定數量的已解決操作（十）之後，在冪或平方根的解決方案之間進行獎勵操作，這將在每次解決生命點時給出，這將累積到那些在場的人（在每個新遊戲中，玩家將有 5 個生命值可用）。" +
                                        "每完成一個操作，正確答案的計數器就會增加，而對於每個錯誤答案，錯誤答案的計數器就會增加，並且會奪走一條生命（當生命結束時，遊戲也將結束）。" +
                                        "如果該操作被認為太難解決，您可以使用跳過按鈕，它可以讓您跳到下一個操作而不影響分數。" +
                                        "關於玩家必須提供的結果的除法，必須以兩位小數給出，即使結果是整數（例如：2.34；3.00）。";
                                        break;
                case "es": this.label1.Text = "¡¡Bienvenidos a MathTrap!!";
                    this.textcell1.Text="Reglas del juego: MathTrap se basa en la solución de las cuatro operaciones fundamentales de la aritmética(suma, resta, multiplicación y división). " +
                                        "después de un número predeterminado de operaciones resueltas(diez), una operación de bonificación entre la solución de una potencia o una raíz cuadrada, que dará cada vez que se resuelva un punto de vida, que se acumulará a los presentes(en cada nuevo juego el jugador tener 5 puntos de vida disponibles). " +
                                        "Por cada operación resuelta se incrementará un contador de respuestas correctas, mientras que por cada respuesta incorrecta se incrementará el contador de respuestas incorrectas y se quitará una vida(cuando las vidas terminen, el juego también terminará). " +
                                        "Si la operación se considera demasiado difícil de resolver, puede utilizar el botón de salto, que le permite pasar a la siguiente operación sin afectar la puntuación. " +
                                        "En cuanto a la división en cuanto al resultado que debe proporcionar el jugador, este debe darse con dos decimales, aun cuando el resultado sea entero (ej.: 2.34; 3.00).";
                                        break;
                default: this.label1.Text = "Welcome on MathTrap!!";
                         this.textcell1.Text="Game rules: MathTrap is based on the solution of the four fundamental operations of arithmetic(addition, subtraction, multiplication and division). " +
                                             "After a predetermined number of resolved operations(ten), a bonus operation between the solution of a power or a square root, which will give each time a life point is resolved, which will accumulate to those present(at each new game the player will have 5 life points available). " +
                                             "For each solved operation a counter of correct answers will be increased, while for each wrong answer the counter of wrong answers will be increased and a life will be taken away(when the lives end, the game will also end). " +
                                             "If the operation is considered too difficult to solve, you can use the skip button, which allows you to skip to the next operation without affecting the score. " +
                                             "Regarding the division as regards the result that the player must provide, this must be given with two decimals, even when the result is integer (eg: 2.34; 3.00).";
                                             break;
            }
        }

        async private void onMain(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage(),false);
        }
    }
}