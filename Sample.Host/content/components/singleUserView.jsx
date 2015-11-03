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

  render() {

    var user = this.state.user;

    if (user == null)
      return (<h1>Unknown user {this.props.id}</h1>);

    var roles = user.roles.map(function(role, index) {
      return (<li key={index} className="role-pill"><a href="#">{role.name}</a></li>);
    });

    return (
      <div>
        <h1>{user.name}<small className="pull-right">{user.key}</small></h1>

        <h4>Roles</h4>
        <hr />
        <ul className="nav nav-pills">
          {roles}
        </ul>

        <h4>Permissions</h4>
        <hr />
        <div className="row"></div>


      </div>
    );
  }

});
