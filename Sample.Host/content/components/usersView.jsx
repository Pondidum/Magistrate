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
          <div key={index} className="col-md-3">
            <div className="panel panel-default">
              <div className="panel-heading">
                <h3 className="panel-title">{user.name}<span className="glyphicon glyphicon-remove-circle pull-right"></span></h3>
              </div>
              <div className="panel-body">
                <div>Permissions: 15</div>
                <div>Roles: 3</div>
              </div>
            </div>
          </div>
        );
      });

    return (
      <div>
        <FilterBar filterChanged={this.filterChanged} />
        <div className="row">
            {users}
        </div>
      </div>
    );
  }

});
