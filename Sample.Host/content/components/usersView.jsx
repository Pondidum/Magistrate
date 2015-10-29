var UsersView = React.createClass({

  getInitialState() {
    return {
      filter: "",
      users: []
    };
  },

  componentDidMount() {
    this.getUsers();
  },

  getUsers() {

    $.ajax({
      url: "/api/users/all",
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({
          users: data || []
        });
      }.bind(this),
      error: function(xhr, status, err) {
        console.error(this.props.url, status, err.toString());
      }.bind(this)
    });

  },

  filterChanged(value) {
    this.setState({
      filter: value
    });
  },

  render() {

    var filter = new RegExp(this.state.filter, "i");

    var users = this.state.users
      .filter(function(user) {
        return user.name.search(filter) != -1;
      })
      .map(function(user, index) {
        return (
          <UserTile key={index} user={user} />
        );
      });

    return (
      <div>
        <div className="row" style={{ marginBottom: "1em" }}>
          <ActionsBar className="col-md-7" />
          <FilterBar filterChanged={this.filterChanged} className="pull-right col-md-5" />
        </div>
        <div className="row">
            {users}
        </div>
      </div>
    );
  }

});
