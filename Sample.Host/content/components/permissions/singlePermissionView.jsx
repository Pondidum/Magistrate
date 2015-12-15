var SinglePermissionView = React.createClass({

  getInitialState() {
    return {
      permission: null
    };
  },

  componentDidMount() {
    this.getPermission();
  },

  getPermission() {

    $.ajax({
      url: this.props.url,
      cache: false,
      success: function(data) {
        this.setState({
          permission: data
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

        var permission = this.state.permission;
        permission.name = newName;

        this.setState({ permission: permission });

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

        var permission = this.state.permission;
        permission.description = newDescription;

        this.setState({ permission: permission });

      }.bind(this)
    });

  },

  render() {

    var permission = this.state.permission;
    var self = this;

    if (permission == null)
      return (<h1>Unknown permission {this.props.permissionKey}</h1>);

    return (
      <div className="well">

        <h1><InlineEditor initialValue={permission.name} onChange={this.onNameChanged} /></h1>
        <div><InlineEditor initialValue={permission.description} onChange={this.onDescriptionChanged} /></div>

      </div>
    )
  }

});