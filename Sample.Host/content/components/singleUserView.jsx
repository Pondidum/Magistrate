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
      url: "/api/users/" + this.props.id,
      cache: false,
      success: function(data) {
        this.setState({
          user: data
        });
      }.bind(this),
      error: function(xhr, status, err) {
        console.error(this.props.url, status, err.toString());
      }.bind(this)
    });

  },

  onIncludeRemoved(permission) {
    var user = this.state.user;
    var newCollection = user.includes.filter(function(inc) {
      return inc.id != permission.id;
    });

    user.includes = newCollection;

    this.setState({
      user: user
    });
  },

  onRevokeRemoved(permission) {
    var user = this.state.user;
    var newCollection = user.revokes.filter(function(inc) {
      return inc.id != permission.id;
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
      return (<h1>Unknown user {this.props.id}</h1>);

    var roles = user.roles.map(function(role, index) {
      return (<li key={index} className="role-pill"><a href="#">{role.name}</a></li>);
    });

    var includes = user.includes.map(function(permission, index) {
      return (
        <li key={index} className="col-sm-3 ">
          <PermissionPill permission={permission}>
            <RemovePermission
              permission={permission}
              onRemove={self.onIncludeRemoved}
              url={"/api/users/" + user.key + "/include/" + permission.key}
              action="Remove"
              from={user.name}
            />
          </PermissionPill>
        </li>
      );
    });

    var revokes = user.revokes.map(function(permission, index) {
      return (
        <li key={index} className="col-sm-3 ">
          <PermissionPill permission={permission}>
            <RemovePermission
              permission={permission}
              onRemove={self.onRevokeRemoved}
              url={"/api/users/" + user.key + "/revoke/" + permission.key}
              action="Remove"
              from={user.name}
            />
          </PermissionPill>
        </li>
      );
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

        <div className="page-header">
          <h4>Includes</h4>
        </div>
        <ul className="list-unstyled list-inline row">
          {includes}
        </ul>

        <div className="page-header">
          <h4>Revokes</h4>
        </div>
        <ul className="list-unstyled list-inline row">
          {revokes}
        </ul>

      </div>
    );
  }

});
