using AppJogoForca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJogoForca.Repositories {
    public class WordRepositories {
        private List<Word> _words;

        public WordRepositories() {
            _words = new List<Word>();
            _words.Add(new Word("Nome", "JOAO".ToUpper()));
            _words.Add(new Word("Nome", "CAROL".ToUpper()));
            _words.Add(new Word("Nome", "PAULO".ToUpper()));
            _words.Add(new Word("Nome", "RENATA".ToUpper()));
            _words.Add(new Word("Vegetal", "BATATA".ToUpper()));
            _words.Add(new Word("Vegetal", "ALFACE".ToUpper()));
            _words.Add(new Word("Vegetal", "BERINJELA".ToUpper()));
            _words.Add(new Word("Vegetal", "ESPINAFRE".ToUpper()));
            _words.Add(new Word("Fruta", "BANANA".ToUpper()));
            _words.Add(new Word("Fruta", "MANGA".ToUpper()));
            _words.Add(new Word("Fruta", "PERA".ToUpper()));
            _words.Add(new Word("Fruta", "MORANGO".ToUpper()));
            _words.Add(new Word("Tempero", "PAPRICA".ToUpper()));
            _words.Add(new Word("Tempero", "COMINHO".ToUpper()));
            _words.Add(new Word("Tempero", "CURCUMA".ToUpper()));
            _words.Add(new Word("Tempero", "OREGANO".ToUpper()));


        }
        public Word GetRandomWord() {
          Random rand = new Random();
          var number = rand.Next(0, _words.Count);
          return _words[number];
        }
    }
}
