using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace HashTableShared {
    public class Element {
        public string Key { get; set; } = "";
        public string Value { get; set; } = "";

        public Element(string key = "", string value = "") {
            Key = key;
            Value = value;
        }

        public override bool Equals(object obj) {
            return obj is Element element &&
                   Key == element.Key &&
                   Value == element.Value;
        }
    }

    public class HashTable {

        public IEnumerable<Element> Elements => table?.Where(item => item.Count != 0).SelectMany(item => item).ToList();

        List<List<Element>> table;
        private int Size { get; }

        public HashTable(int inputSize = 1000) {
            Size = inputSize;
            table = new List<List<Element>>(inputSize);
            while (inputSize-- != 0) {
                table.Add(new List<Element> ());
            }
        }

        private int HashCode(string key, int a) =>
            (HashCode1(key) + a * HashCode2(key)) % Size;
   

        private int HashCode1(string key) =>
            (key[0] * key[1] * 2 + key[1] * key[2] * 10 + key[2] * key[3] * 27 + key[3] * key[4] * 82 + key[4] * key[5] * 242) % Size;
       

        private int HashCode2(string key) =>
            (key[0] * 3 + key[1] * 9 + key[2] * 27 + key[3] * 81 + key[4] * 243 + key[5] * 729) % Size;

        private static void KeyIsCorrect(string key) {
            if (key[0] < 'A' || key[0] > 'Z' ||
                key[1] < 'A' || key[1] > 'Z' ||
                key[2] < '0' || key[2] > '9' ||
                key[3] < '0' || key[3] > '9' ||
                key[4] < 'A' || key[4] > 'Z' ||
                key[5] < 'A' || key[5] > 'Z') {
                throw new ArgumentException();
            }
        }

        private void Add(Element toAdd) {
            KeyIsCorrect(toAdd.Key);

            var count = 0;
            while (count != Size * 3) {
                if ((table[HashCode(toAdd.Key, count)].Count == 0 || table[HashCode(toAdd.Key, count)][0].Key == toAdd.Key)) {
                    table[HashCode(toAdd.Key, count)].Add(toAdd);
                    break;
                }
                // В этой строке поменять == на !=

                if (table[HashCode(toAdd.Key, count)][0].Key == "") {
                    table[HashCode(toAdd.Key, count)][0] = toAdd;
                    break;
                }
                count++;
            }

            if (count == Size * 3) {
                throw new CheckoutException();
            }
        }


        public string this[string key] {
            set => Add(new Element(key, value));

            get {
                KeyIsCorrect(key);

                var count = 0;
                while (count != Size * 3) {
                    if (table[HashCode(key, count)][0].Key == key) {
                        break;
                    }

                    count++;
                }

                if (count == Size * 3) {
                    throw new CheckoutException();
                }

                return table[HashCode(key, count)].FirstOrDefault()?.Value;
            }
        }
    }
}

