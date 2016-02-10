var collection = {

  add(collection, item) {
    return collection.concat([item]);
  },

  remove(collection, item) {
    return collection.filter(x => x.key !== item.key);
  },

  change(collection, added, removed) {

    return collection
      .concat(added)
      .filter(x => removed.find(y => y.key == x.key) == null);
  },

}
