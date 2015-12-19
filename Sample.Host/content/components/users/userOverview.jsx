var UserOverview = React.createClass({

  getInitialState() {
    return {
      collection: []
    };
  },

  componentDidMount() {
    this.loadCollection();
  },

  loadCollection() {

    $.ajax({
      url: this.props.url + "/all",
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({
          collection: data || []
        });
      }.bind(this)
    });

  },

  render() {

    return (
      <Overview
        tile={UserTile}
        create={CreateUserDialog}
        collection={this.state.collection}
        {...this.props}
      />
    );

  }
});
