var SingleRoleView = React.createClass({

  getInitialState() {
    return {
      role: null
    };
  },

  componentDidMount() {
    this.getRole();
  },

  getRole() {

    $.ajax({
      url: this.props.url,
      cache: false,
      success: function(data) {
        this.setState({
          role: data
        });
      }.bind(this)
    });

  },

  onNameChanged(newName) {

    var json = JSON.stringify({
      name: newName
    });

    $.ajax({
      url: this.props.url + "/name",
      cache: false,
      method: "PUT",
      data: json,
      success: function() {

        var role = this.state.role;
        role.name = newName;

        this.setState({ role: role });

      }.bind(this)
    });

  },

  onDescriptionChanged(newDescription) {

    var json = JSON.stringify({
      description: newDescription
    });

    $.ajax({
      url: this.props.url + "/description",
      cache: false,
      method: "PUT",
      data: json,
      success: function() {

        var role = this.state.role;
        role.description = newDescription;

        this.setState({ role: role });

      }.bind(this)
    });

  },

  render() {

    var role = this.state.role;
    var self = this;

    if (role == null)
      return (<h1>Unknown role {this.props.roleKey}</h1>);

    return (
      <div className="well">

        <h1><InlineEditor initialValue={role.name} onChange={this.onNameChanged} /></h1>
        <div><InlineEditor initialValue={role.description} onChange={this.onDescriptionChanged} /></div>

        <PermissionGrid
          collection={role.permissions}
          navigate={this.props.navigate}
          url={this.props.url + "/permissions/"}
        />

      </div>
    )
  }
});
