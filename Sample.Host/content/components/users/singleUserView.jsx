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
      url: this.props.url,
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

    return (
      <div className="well">
        <h1>{user.name}<small className="pull-right">{user.key}</small></h1>

        <RoleGrid
          collection={user.roles}
          navigate={this.props.navigate}
          url={this.props.url + "/roles/"}
          name="Roles"
        />

        <PermissionGrid
          collection={user.includes}
          navigate={this.props.navigate}
          url={this.props.url + "/includes/"}
          name="Includes"
        />

        <PermissionGrid
          collection={user.revokes}
          navigate={this.props.navigate}
          url={this.props.url + "/revokes/"}
          name="Revokes"
        />

      </div>
    );
  }

});
