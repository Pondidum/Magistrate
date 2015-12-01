var Overview = React.createClass({

  getInitialState() {
    return {
      filter: "",
      collection: [],
    };
  },

  componentDidMount() {
    this.loadCollection();
  },

  loadCollection() {

    $.ajax({
      url: this.props.url,
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({
          collection: data || []
        });
      }.bind(this),
      error: function(xhr, status, err) {
        console.error(this.props.url, status, err.toString());
      }.bind(this)
    });

  },

  onFilterChanged(value) {
    this.setState({
      filter: value
    });
  },

  onAdd(item) {
    var newCollection = this.state.collection.concat([item]);

    this.setState({
      collection: newCollection
    });
  },

  onRemove(item) {

    var newCollection = this.state.collection.filter(function(x) {
      return x.key !== item.key
    });

    this.setState({
      collection: newCollection
    });

  },

  render() {

    var self = this;
    var filter = new RegExp(this.state.filter, "i");

    var collection = this.state.collection
      .filter(function(item) {
        return item.name.search(filter) != -1;
      })
      .map(function(item, index) {
        return (
          <self.props.tile
            key={index}
            content={item}
            onRemove={self.onRemove}
            navigate={self.props.navigate}
          />
        );
      });

    var create = (<self.props.create onCreate={this.onAdd} />);

    return (
      <div>
        <ContentArea filterChanged={this.onFilterChanged} actions={[create]}>
          {collection}
        </ContentArea>
      </div>
    );

  }

});
