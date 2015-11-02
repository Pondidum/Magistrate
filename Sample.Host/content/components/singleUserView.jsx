var SingleUserView = React.createClass({

  getInitialState() {
    return {
      user: {}
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

    return (
      <div>
        <h1>{user.name}</h1>

        <h4>Roles</h4>
        <hr />
        <div className="row"></div>

        <h4>Permissions</h4>
        <hr />
        <div className="row"></div>


      </div>
    );
  }

});
