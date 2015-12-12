var SingleUserView = React.createClass({

  getInitialState() {
    return {
      user: null
    };
  },

  componentDidMount() {
    this.getUser();
  },

  getUser() {

    $.ajax({
      url: "/api/users/" + this.props.userKey,
      cache: false,
      success: function(data) {
        this.setState({
          user: data
        });
      }.bind(this)
    });

  },

  onIncludeRemoved(permission) {
    var user = this.state.user;
    var newCollection = user.includes.filter(function(inc) {
      return inc.key != permission.key;
    });

    user.includes = newCollection;

    this.setState({
      user: user
    });
  },

  onRevokeRemoved(permission) {
    var user = this.state.user;
    var newCollection = user.revokes.filter(function(inc) {
      return inc.key != permission.key;
    });

    user.revokes = newCollection;

    this.setState({
      user: user
    });
  },

  render() {

    var user = this.state.user;
    var self = this;

    if (user == null)
      return (<h1>Unknown user {this.props.userKey}</h1>);

    var roles = user.roles.map(function(role, index) {
      return (<li key={index} className="role-pill"><a href="#">{role.name}</a></li>);
    });

    return (
      <div className="well">
        <h1>{user.name}<small className="pull-right">{user.key}</small></h1>

        <div className="page-header">
          <h4>Roles</h4>
        </div>
        <ul className="nav nav-pills">
          {roles}
        </ul>

        <PermissionGrid
          permissions={user.includes}
          navigate={this.props.navigate}
          url={"/api/users/" + user.key + "/includes/"}
          name="Includes"
        />

        <PermissionGrid
          permissions={user.revokes}
          navigate={this.props.navigate}
          url={"/api/users/" + user.key + "/revokes/"}
          name="Revokes"
        />

      </div>
    );
  }

});
