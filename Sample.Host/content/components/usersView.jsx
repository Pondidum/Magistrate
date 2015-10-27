var UsersView = React.createClass({

  getInitialState() {
    return {
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

  render() {

    var users = this.state.users.map(function(user, index) {
      return (
        <tr key={index}>
          <td>{user.name}</td>
          <td>
            <a className="btn btn-default" href="#" role="button">Menu</a>
          </td>
        </tr>
      );
    });

    return (
      <div>
        <h1>Users</h1>
        <table className="table">
          <thead>
            <tr>
              <th className="col-md-10">Name</th>
              <th className="col-md-2">Actions</th>
            </tr>
          </thead>
          <tbody>
            {users}
          </tbody>
        </table>
      </div>
    );
  }

});
